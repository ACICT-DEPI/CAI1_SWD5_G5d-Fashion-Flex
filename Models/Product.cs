using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
    public class Product
    {
        public int Id { get; set; }
        [ForeignKey("Brand")]
        public int Brand_Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Available_Quantity { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public double Discount { get; set; }
        public DateTime Added_Date { get; set; }

        //Order_Item OM--refersTo-->|1 Product
        public virtual List<Order_Item>? Order_Items { get; set; }
        //Product |1--Recevice-->OM Review
        public virtual List<Review>? Reviews { get; set; }
        //Product OM--Own-->|1 Brand
        public virtual Brand Brand { get; set; }
    }
}
