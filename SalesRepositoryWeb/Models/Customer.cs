using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRepositoryWeb.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "max length is 20")]
        public string Lastname { get; set; }
        
        public virtual  ICollection<Sale> Sales { get; set; }
    }
}