using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options)
            : base(options)
        {

        }

        public DbSet<Job> Jobs { get; set; }
    }
}
