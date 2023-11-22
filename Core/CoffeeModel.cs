using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CoffeeModel
    {
        [Key]
        public int CoffeeId { get; set; }   
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set;}
    }
}
