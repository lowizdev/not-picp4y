using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Common.Models.DTO;
using Common.Models.RabbitMQ;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimplePayment.Services
{

    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> CreatePayment(PaymentDTO paymentDTO)
        {

            #region Payment Pre-Validation

            

            #endregion


            Payment payment = new(
                Guid.NewGuid().ToString(),
                paymentDTO.Value,
                paymentDTO.Payer,
                paymentDTO.Payee,
                EPaymentStatus.Created);

            payment = await _paymentRepository.InsertPayment(payment);

            bool result = false;

            if (payment != null)
            {

                result = await PublishPaymentCreated(payment);


            }

            return result;


        }

        private async Task<bool> PublishPaymentCreated(Payment payment)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "payment_queue", durable: true, exclusive: false,
                    autoDelete: false, arguments: null);

                PaymentCreatedMessage message = new();

                message.PaymentIdentifier = payment.PaymentIdentifier; //TODO: TO AVOID DEPENDING ON A COMMON DATABASE, THE FULL OBJECT COULD BE SENT
                message.PublishedDate = DateTime.Now;

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                var properties = new BasicProperties
                {
                    Persistent = true
                };

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "payment_queue", mandatory: true,
                    basicProperties: properties, body: body);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;


        }

    }
}
