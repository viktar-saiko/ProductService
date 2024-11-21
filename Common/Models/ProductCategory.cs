namespace Common.Models
{
    /// <summary>
    /// The model of Category is used by API request
    /// </summary>
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
