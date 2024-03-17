using ASP_66Bit_Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP_66Bit_Test.Data
{
    public class AppDBContext:DbContext
    {
        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Team> Teams { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) 
            : base(options) { }
    }
}
