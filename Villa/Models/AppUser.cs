using Microsoft.AspNetCore.Identity;

namespace Villa.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
