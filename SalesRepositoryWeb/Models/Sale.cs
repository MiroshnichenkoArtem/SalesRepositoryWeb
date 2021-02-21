using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesRepositoryWeb.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public virtual int? ManagerId { get; set; }
        public virtual int? CustomerId { get; set; }
        public virtual int? ProductId { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual  Customer Customer { get; set; }
        public virtual Product Product { get; set; }

    
    }
}