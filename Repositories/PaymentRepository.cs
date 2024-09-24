using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.IRepositories.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        static Random rng = new Random();
        private readonly FFContext context;
        public PaymentRepository(FFContext context)
        {
            this.context = context;
        }
        public void Add(Payment newpayment)
        {
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

        public string generaterandomnum()
        {
            int min = 1;
            int max = 100;

            List<int> numbers = new List<int>();

            for (int i = min; i < max; i++)
            {
                numbers.Add(i);
            }

            // Shuffle the list
            Shuffle(numbers);
            return numbers.ToString();

        }
        

        static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T value = list[n];
                list[n] = list[k];
                list[k] = value;
            }
        }



        public void updateOrderStates(int paymentid)
        {
            var payment = GetById(paymentid);
            if (payment != null)
            {

                payment.Payment_Status = "Paid";
                payment.Payment_Date = DateTime.Now;
                payment.Transaction_Id = generaterandomnum();

            }
            else
            {
                throw new InvalidOperationException($"Payment with id:{paymentid} is not found");
            }
        }

    }
}
