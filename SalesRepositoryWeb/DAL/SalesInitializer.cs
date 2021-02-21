using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesRepositoryWeb.Models;

namespace SalesRepositoryWeb.DAL
{
    public class SalesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SalesContext>
    {
        protected override void Seed(SalesContext context) 
        {
            var customers = new List<Customer>
            {
                new Customer {Lastname = "John"},
                new Customer {Lastname = "Freddy"},
                new Customer {Lastname = "Artem"}
            };
            customers.ForEach(x => context.Customers.Add(x));
            context.SaveChanges();
            var managers = new List<Manager>
            {
                new Manager {Lastname = "JohnManager"},
                new Manager {Lastname = "FreddyManager"},
                new Manager {Lastname = "ArtemManager"}
            };
            managers.ForEach(x => context.Managers.Add(x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product {Name = "Product1"},
                new Product {Name = "Product2"},
                new Product {Name = "Product3"}
            };
            products.ForEach(x => context.Products.Add(x));
            context.SaveChanges();

            var sales = new List<Sale>
            {
                new Sale
                {
                    Date = DateTime.Now, Manager = managers[0], Customer = customers[0], Product = products[0],
                    Price = 0.12
                },
                new Sale
                {
                    Date = DateTime.Now, Manager = managers[1], Customer = customers[1], Product = products[1],
                    Price = 0.13
                },
                new Sale
                {
                    Date = DateTime.Now, Manager = managers[2], Customer = customers[2], Product = products[2],
                    Price = 0.14
                },
                

            };
            sales.ForEach(x => context.Sales.Add(x));
            context.SaveChanges();




        }
    }
}
