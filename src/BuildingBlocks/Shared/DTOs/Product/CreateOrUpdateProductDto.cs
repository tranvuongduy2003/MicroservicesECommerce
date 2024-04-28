using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public class CreateOrUpdateProductDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Maximun length for Product Name is 250 characters.")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Maximun length for Product Summary is 250 characters.")]
        public string Summary { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
