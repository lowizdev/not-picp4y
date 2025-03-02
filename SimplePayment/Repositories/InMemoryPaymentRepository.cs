using Common.Models;

namespace SimplePayment.Repositories
{
    public class InMemoryPaymentRepository
    {
        //DEVELOPMENT ONLY
        private static List<Payment> _payments = new();

        public InMemoryPaymentRepository()
        {
            
        }



    }
}
