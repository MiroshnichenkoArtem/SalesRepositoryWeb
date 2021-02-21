using System.Data.Entity;
using SalesRepositoryWeb.Models;

namespace SalesRepositoryWeb.DAL
{
    public class SalesContext : DbContext
    {
        public SalesContext() : base("SalesDbConnection")
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}

