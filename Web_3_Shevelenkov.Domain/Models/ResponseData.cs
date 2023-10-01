using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_3_Shevelenkov.Domain.Models
{
    public class ResponseData<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
