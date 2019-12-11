using accounts.DAL.models;
using System.ComponentModel.DataAnnotations;

namespace accounts.core.Dto
{
    public class AccountTransferDto
    {

        [Required]
        [StringLength(18)]
        public string FromAccountNo { get; set; }
        public Account FromAccount { get; set; }
        public decimal Amount { get; set; }

        [Required]
        [StringLength(18)]
        public string ToAccountNo { get; set; }
        public Account ToAccount { get; set; }
    }
}
