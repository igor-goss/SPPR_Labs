using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_3_Shevelenkov.Domain.Entities
{
    public class Tank
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TankType? Type { get; set; }
        public decimal Price { get; set; }
        public string? Path {  get; set; } 
    }
}
