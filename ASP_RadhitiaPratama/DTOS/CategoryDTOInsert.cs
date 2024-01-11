using System.ComponentModel.DataAnnotations;

namespace ASP_RadhitiaPratama.DTOS
{
    public class CategoryDTOInsert
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
