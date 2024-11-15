using System.ComponentModel.DataAnnotations;

namespace task2API.DTOs.ProductsDto_s
{
    public class DtoProductsCreateUpdate
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
        [MaxLength(30, ErrorMessage = "Name must not exceed 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(20, 3000, ErrorMessage = "Price must be between 20 and 3000.")]
        public int Price { get; set; }
    }
}
