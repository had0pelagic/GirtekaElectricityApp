using GirtekaElectricityDomain;
using Microsoft.EntityFrameworkCore;

namespace GirtekaElectricityInfrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Electricity> Electricity { get; set; }
    }
}