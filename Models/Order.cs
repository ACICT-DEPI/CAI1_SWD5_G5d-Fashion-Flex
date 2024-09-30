using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fashion_Flex.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }
        [ForeignKey("Payment")]
        public int? Payment_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public string Order_Status { get; set; }
        public string Shipping_Address { get; set; }
        public int Tracking_Number { get; set; }

        //Customer |1--Places-->OM Order
        public virtual Customer Customer { get; set; }
        //Order |1--Has-->O1 Payment
        public virtual Payment? Payment { get; set; }
        //Order |1--Includes-->|M Order_Item
        public virtual List<Order_Item> Order_Items { get; set; }
    }
}
