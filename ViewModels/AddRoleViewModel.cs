using System.ComponentModel.DataAnnotations;

namespace Fashion_Flex.ViewModels
{
	public class AddRoleViewModel
	{
		[Required]
		public string RoleName { get; set; }
	}
}
