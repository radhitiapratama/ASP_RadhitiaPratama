using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_RadhitiaPratama.DTOS
{
    public class TransactionDTOInsert
    {
        [Required]
        public string CustomerName { get; set; } = null!;

        [Required, DefaultValue("yyyy-MM-ddTHH:mm:ss")]
        public DateTime TransactionDate { get; set; }

        public List<OrderDTOInsert> orders { get; set; }
    }
}
