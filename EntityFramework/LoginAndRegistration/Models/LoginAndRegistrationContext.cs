using LoginAndRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAndRegistration.Models
{
    public class LoginAndRegistrationContext : DbContext
    {
        public LoginAndRegistrationContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
