using System.ComponentModel.DataAnnotations;

namespace ASP_RadhitiaPratama.DTOS
{
    public class OrderDTOInsert
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, double.MaxValue)]
        public int Qty { get; set; }
    }
}
