using System.Collections;
using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class SweetExpanseDataDto
    {
        public IEnumerable<Sweet> Sweets { get; set; }
        
        public Dictionary<string, int> Counts { get; set; }
    }
}