using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweets.ApiModels
{
    public class SweetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
