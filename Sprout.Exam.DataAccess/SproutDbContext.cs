using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Entities;

namespace Sprout.Exam.DataAccess
{
    public class SproutDbContext : DbContext
    {
        public SproutDbContext(DbContextOptions<SproutDbContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }
    }
}
