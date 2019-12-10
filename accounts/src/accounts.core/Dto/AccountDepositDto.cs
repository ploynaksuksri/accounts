using accounts.DAL.models;
using System.ComponentModel.DataAnnotations;

namespace accounts.core.Dto
{
    public class AccountDepositDto
    {
        public int AccountId { get; set; }

        [Required]
        [StringLength(18)]
        public string AccountNo { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }


}
