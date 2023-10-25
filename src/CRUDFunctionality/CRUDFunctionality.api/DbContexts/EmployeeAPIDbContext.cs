using CRUDFunctionality.api.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDFunctionality.api.DbContexts
{
    public class EmployeeAPIDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public EmployeeAPIDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseAPI"));
        }
        //public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<AddPersonalDataDbModel> AddPersonalData
        {
            get;
            set;
        }
        public DbSet<AddOfficialDataDbModel> AddOfficialData
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddPersonalDataDbModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<AddOfficialDataDbModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            

            
            

        }
    }

}