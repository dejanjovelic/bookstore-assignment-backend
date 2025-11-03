using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Services
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
