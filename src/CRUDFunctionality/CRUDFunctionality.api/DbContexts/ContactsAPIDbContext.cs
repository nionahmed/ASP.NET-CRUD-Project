using CRUDFunctionality.api.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDFunctionality.api.DbContexts
{
    public class ContactsAPIDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ContactsAPIDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
        //public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts
        {
            get;
            set;
        }
    }

}