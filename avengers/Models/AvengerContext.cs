using Microsoft.EntityFrameworkCore;

namespace avengers.Models
{
    public class AvengerContext : DbContext
    {
        public AvengerContext (DbContextOptions<AvengerContext> options)
            : base(options)
        {
        }

        public DbSet<AvengerItem> AvengerItems { get; set; }
    }
}
