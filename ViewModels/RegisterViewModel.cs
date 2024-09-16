using System.ComponentModel.DataAnnotations;

namespace Fashion_Flex.ViewModels
{
    public class RegisterViewModel
    {
        // Identity Fields
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        // Customer Fields
        [Required]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly Date_Of_Birth { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Phone Country Code")]
        public string? Phone_Country_Code { get; set; }

        public bool Is_Active { get; set; } = true; // Default is active for new customers

    }
}
