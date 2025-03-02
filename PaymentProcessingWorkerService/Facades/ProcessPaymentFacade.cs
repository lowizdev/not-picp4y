using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Common.Models.RabbitMQ;
using Common.Models.SQLite;
using PaymentProcessingWorkerService.Repositories;
using PaymentProcessingWorkerService.Repositories.UnityOfWork;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentProcessingWorkerService.Facades
{
    public interface IProcessPaymentFacade
    {
        Task ConsumeItem();
    }

    public class ProcessPaymentFacade : IProcessPaymentFacade
    {

        private readonly IUnityOfWork _unityOfWork;
        private readonly IQueryPaymentRepository _queryPaymentRepository;
        private readonly IQueryUserRepository _queryUserRepository;
        private readonly IWalletFullRepository _walletFullRepository;

        public ProcessPaymentFacade(
            IUnityOfWork unityOfWork,
            IQueryPaymentRepository queryPaymentRepository,
            IQueryUserRepository queryUserRepository,
            IWalletFullRepository walletFullRepository)
        {
            _unityOfWork = unityOfWork;
            _queryPaymentRepository = queryPaymentRepository;
            _queryUserRepository = queryUserRepository;
            _walletFullRepository = walletFullRepository;
        }

        public async Task ConsumeItem()
        {

            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "payment_queue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);

            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {

                try
                {
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    PaymentCreatedMessage paymentCreatedMessage = JsonSerializer
                        .Deserialize<PaymentCreatedMessage>(message);

                    bool pagamentoProcesado = await ProcessPayment(paymentCreatedMessage);

                    Console.WriteLine(" [x] Done");

                    // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
                    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false); //TODO: FIX ACK HANDLING
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            };

            await channel.BasicConsumeAsync("payment_queue", autoAck: false, consumer: consumer);

        }

        public async Task<bool> ProcessPayment(PaymentCreatedMessage paymentCreatedMessage)
        {
            //TODO: IMPLEMENT 'PAYMENT STATUS' HANDLING, UPDATE AND ERROR TREATMENT

            _unityOfWork.BeginTransaction();

            PaymentSQLiteDTO paymentSQLiteDTO = await _queryPaymentRepository
                .QuerySinglePaymentById(paymentCreatedMessage.PaymentIdentifier);

            if (paymentSQLiteDTO == null)
            {

                return false;

            }

            UserSQLiteDTO payer = await _queryUserRepository
                .QueryUserById(paymentSQLiteDTO.payerid);

            if (payer == null)
            {

                return false;

            }
            else if (payer.usertype == (int)EUserType.Store)
            {

                return false;

            }

            UserSQLiteDTO payee = await _queryUserRepository
                .QueryUserById(paymentSQLiteDTO.payeeid);

            if (payee == null)
            {

                return false;

            }


            WalletSQLiteDTO payerWallet = await _walletFullRepository
                .QuerySingleWalletByUserId(payer.id);

            if (payerWallet == null)
            {
                return false;
            }
            else if (payerWallet.value < paymentSQLiteDTO.value)
            {
                return false;
            }

            WalletSQLiteDTO payeeWallet = await _walletFullRepository
                .QuerySingleWalletByUserId(payee.id);

            if (payeeWallet == null)
            {
                return false;
            }

            decimal updatedPayeeWalletValue = payeeWallet.value + paymentSQLiteDTO.value;

            Wallet updatedPayeeWallet = new Wallet(
                payeeWallet.id,
                payeeWallet.userid,
                updatedPayeeWalletValue
                );

            bool isPayeeWalletUpdated = await _walletFullRepository.UpdateWallet(updatedPayeeWallet);

            decimal updatedPayerWalletValue = payerWallet.value - paymentSQLiteDTO.value;

            Wallet updatedPayerWallet = new Wallet(
                payerWallet.id,
                payerWallet.userid,
                updatedPayerWalletValue
                );

            bool isPayerWalletUpdated = await _walletFullRepository.UpdateWallet(updatedPayerWallet);

            if (!isPayeeWalletUpdated || !isPayerWalletUpdated)
            {
                _unityOfWork.Rollback();
                return false;
            }

            _unityOfWork.Commit();
            return true;

        }

    }
}
