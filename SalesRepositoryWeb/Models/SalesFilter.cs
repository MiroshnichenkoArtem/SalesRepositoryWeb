using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesRepositoryWeb.Models
{
    public class OrderFilter
    {
        [Display(Name = "CustomerLastname")]
        public string CustomerLastname { get; set; }

        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
    }
}