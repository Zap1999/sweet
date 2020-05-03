namespace SweetLife.Models
{
    public partial class SweetIngredient
    {
        public long SweetId { get; set; }
        public long IngredientId { get; set; }
        public decimal Count { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Sweet Sweet { get; set; }
    }
}
