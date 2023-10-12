using Microsoft.EntityFrameworkCore;
using Employee360.Web.Models;

namespace Employee360.Web.DbContexts
{
    public class AddToDatabaseDbContexts : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AddToDatabaseDbContexts(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Employee"));
        }
        //public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<EmpPersonalInfo> Emp_Personal_Info
        {
            get;
            set;
        }
    }
}
