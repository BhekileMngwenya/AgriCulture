using System.ComponentModel.DataAnnotations;
using culture.Models;

namespace culture.Models
{
    public class ProductWithFarmerProfileViewModel
    {
        public Product Product { get; set; }

        public FarmerProfile FarmerProfile { get; set; }
    }
}
