using Microsoft.EntityFrameworkCore;
 

namespace BMS.Infrastructure
{
    public class BMSContext : DbContext
    {
        public BMSContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           

        }
    }
}
