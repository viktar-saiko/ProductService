using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Sql.Entities
{
    [Table("products")]
    public class ProductDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Column("description")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("tags")]
        public string Tags { get; set; } = string.Empty;

        //[Column("category_ids")]
        //public ICollection<int> ProductCategoriesIds { get; set; } = new List<int>();
        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
