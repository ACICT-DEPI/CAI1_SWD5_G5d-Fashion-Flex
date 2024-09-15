namespace Fashion_Flex.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateOnly Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; }
        public string Phone_Country_Code { get; set; }
        public bool Is_Active { get; set; }        
    }
}
