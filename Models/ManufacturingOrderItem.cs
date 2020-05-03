namespace SweetLife.Models
{
    public partial class ManufacturingOrderItem
    {
        public long ManufacturingOrderId { get; set; }
        public long SweetId { get; set; }
        public int Count { get; set; }

        public virtual ManufacturingOrder ManufacturingOrder { get; set; }
        public virtual Sweet Sweet { get; set; }
    }
}
