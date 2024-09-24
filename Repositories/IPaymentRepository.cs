using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IPaymentRepository
    {
        public void Add(Payment newpayment);
        public void Update(Payment payment);
        public void Delete(int paymentId);
        public Payment GetById(int paymentId);
        public List<Payment> GetAll();
        public void Save();
        void updateOrderStates(int paymentid);

    }
}