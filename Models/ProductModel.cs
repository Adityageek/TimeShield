using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeShield.Models
{
    public class ProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Product1 { get; set; }

        [Required]
        public int Quantity { get; set; }


    }
}