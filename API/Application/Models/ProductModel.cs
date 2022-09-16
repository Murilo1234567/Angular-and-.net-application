using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(TypeName = "int8")]
        public int Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? Name { get; set; }
        
        [Column(TypeName = "varchar")]
        public string? Image { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string? Description { get; set; }

        [Column(TypeName = "int8")]
        public int Stock { get; set; }

        [Column(TypeName = "bool")]
        [DefaultValue(true)]
        public bool? Status { get; set; }

        [Column(TypeName = "numeric(10, 2)")]
        public decimal Price { get; set; }
    }
}