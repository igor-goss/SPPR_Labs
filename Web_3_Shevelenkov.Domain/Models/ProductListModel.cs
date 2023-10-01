using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_3_Shevelenkov.Domain.Models
{
    public class ProductListModel<T>
    {
        public List<T>? Items { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}
