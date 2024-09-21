using System.ComponentModel.DataAnnotations;

namespace Fashion_Flex.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name ="First Name")]
        public string First_Name { get; set; }
		[Display(Name = "Last Name")]
		public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
		[Display(Name = "Date of Birth")]
		public DateOnly Date_Of_Birth { get; set; }
		[Display(Name = "Phone Number")]
		public string Phone_Number { get; set; }
        public string Phone_Country_Code { get; set; }
        public bool Is_Active { get; set; }        
    }
}
