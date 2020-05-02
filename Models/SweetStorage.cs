using System;
using System.Collections.Generic;

namespace Sweet.Models
{
    public partial class SweetStorage
    {
        public long SweetId { get; set; }
        public long FactoryId { get; set; }
        public long Count { get; set; }

        public virtual Factory Factory { get; set; }
        public virtual Sweet Sweet { get; set; }
    }
}
