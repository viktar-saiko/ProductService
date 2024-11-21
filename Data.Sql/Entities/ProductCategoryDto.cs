using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Sql.Entities
{
    [Table("product_category")]
    public class ProductCategoryDto
    {
        //[Key]
        //[Column(Order = 0)]
        [ForeignKey("products")]
        public Guid ProductId { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [ForeignKey("categories")]
        public int CategoryId { get; set; }

        public ProductDto Product { get; } = null!;
        public CategoryDto Category { get; } = null!;
    }
}
