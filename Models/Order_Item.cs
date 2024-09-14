using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
    public class Order_Item
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public string Order_Status { get; set; }

         //Order |1--Includes-->|M Order_Item
         public virtual Order Order { get; set; }
         //Order_Item OM--refersTo-->|1 Product
         public virtual Product Product { get; set; }
    }
}
