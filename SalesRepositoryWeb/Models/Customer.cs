using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesRepositoryWeb.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        
        public virtual  ICollection<Sale> Sales { get; set; }
    }
}