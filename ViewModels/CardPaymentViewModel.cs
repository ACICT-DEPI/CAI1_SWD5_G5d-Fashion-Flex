using System.ComponentModel.DataAnnotations;

namespace Fashion_Flex.ViewModels
{
    public class CardPaymentViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Cardholder Name")]
        public string CardHolderName { get; set; }

        [Required]
        [Display(Name = "Country or Region")]
        public string? Country_or_Region { get; set; }

        [Required]
        [Display(Name = "Card Information")]
        public string? Cardnumber { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? CVC { get; set; }


    }
}
