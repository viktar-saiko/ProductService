namespace Common.Models
{
    /// <summary>
    /// The model is used by API request
    /// </summary>
    public class Product
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
