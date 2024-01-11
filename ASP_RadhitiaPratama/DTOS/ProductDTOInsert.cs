using System.ComponentModel.DataAnnotations;

namespace ASP_RadhitiaPratama.DTOS
{
    public class ProductDTOInsert
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Brand { get; set; }

        [Required, Range(1, double.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
