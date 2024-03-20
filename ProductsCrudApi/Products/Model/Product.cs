using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCrudApi.Products.Model
{
    [Table("product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int Id { get; set; }
        [Required]
        [Column("name")]

        public string Name { get; set; }
        [Required]
        [Column("price")]

        public double Price { get; set; }
        [Required]
        [Column("stock")]

        public int Stock { get; set; }  
        [Required]
        [Column("producer")]

        public string Producer { get; set; }



    }
}
