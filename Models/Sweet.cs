namespace SweetLife.Models
{
    public partial class Sweet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
