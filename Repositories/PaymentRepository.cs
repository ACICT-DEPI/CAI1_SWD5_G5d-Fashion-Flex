using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly FFContext context;
        public PaymentRepository(FFContext context) 
        {
            this.context = context;
        }
        public void Add(Payment newpayment)
        {
            //context.Payment.Add(obj);
            context.Payment.Add(newpayment);
            
        }
        public void Update(Payment payment)
        {
            context.Payment.Update(payment);
        }
        public void Delete(int paymentId)
        {
            var payment = GetById(paymentId);
            context.Payment.Remove(payment);
        }
        public Payment GetById(int paymentId) 
        {
            return context.Payment.FirstOrDefault(p => p.Id == paymentId);
        }
        public List<Payment> GetAll() 
        {
            return context.Payment.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
