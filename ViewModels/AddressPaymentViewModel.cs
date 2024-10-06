using System.ComponentModel.DataAnnotations;

namespace Fashion_Flex.ViewModels
{
    public class AddressPaymentViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Phone Country Code")]
        public string? Phone_Country_Code { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Full_Name { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }



    }
}
