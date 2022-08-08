using Microsoft.EntityFrameworkCore;

namespace CrudProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CrudProject> CrudProjects { get; set; }
    }
}
