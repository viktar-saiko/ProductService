using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Sql.Entities
{
    [Table("categories")]
    public class CategoryDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = null!;

        //[ForeignKey("ProductId")]
        //public Guid ProductId { get; set; }
        public ICollection<ProductDto> Product { get; } = null!;
    }
}
