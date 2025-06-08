using System.ComponentModel.DataAnnotations;

namespace culture.Models
{
    public class ProductWithFarmerProfileViewModel
    {
        [Required]
        public Product Product { get; set; }
    }
}
