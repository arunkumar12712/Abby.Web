using Abby.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace Abby.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
            
        public DbSet<Category> Category { get; set; }

    }
}
