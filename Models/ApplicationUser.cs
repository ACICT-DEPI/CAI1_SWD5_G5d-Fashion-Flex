using Microsoft.AspNetCore.Identity;

namespace Fashion_Flex.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property to link with Customer entity
        public virtual Customer? Customer { get; set; }
    }
}
