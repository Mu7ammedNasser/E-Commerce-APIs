using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
