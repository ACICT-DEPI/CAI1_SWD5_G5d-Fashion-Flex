using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fashion_Flex.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public DateTime Payment_Date { get; set; }
        public string Payment_Method { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Payment_Status { get; set; }
        public string Transaction_Id { get; set; }


        //Order |1--Has-->O1 Payment
        public virtual Order Order { get; set; }

    }
}
