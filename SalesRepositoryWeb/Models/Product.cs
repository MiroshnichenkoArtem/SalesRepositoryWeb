﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesRepositoryWeb.Models
{
    public class Product
    {
        public  int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "max length is 20")]
        public string Name { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}