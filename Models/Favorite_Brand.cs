using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
    public class Favorite_Brand
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }
        [ForeignKey("Brand")]
        public int Brand_Id { get; set; }

        //Customer |1--Favorites-->OM Favorite_Brand
        public virtual Customer Customer { get; set; }
        //Brand O1--Admire-->OM Favorite_Brand
        public virtual Brand? Brand { get; set; }
    }
}
