using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace culture.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        [Required]
        [ForeignKey("FarmerProfile")]
        public int FarmerProfileId { get; set; }

        public FarmerProfile FarmerProfile { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
