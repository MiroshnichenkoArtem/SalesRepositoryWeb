using System.ComponentModel.DataAnnotations;

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