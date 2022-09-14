namespace Application.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
    }
}