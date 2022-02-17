using Microsoft.EntityFrameworkCore;

namespace Crudelicious.Models
{
    public class CrudeliciousContext:DbContext
    {
        public CrudeliciousContext(DbContextOptions options) : base(options) { }

        // for every model / entity that is going to be part of the db
        // the names of these properties will be the names of the tables in the db
        
        public DbSet<Dish> Dishes { get; set; }
    
    }
}