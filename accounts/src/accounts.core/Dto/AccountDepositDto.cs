using accounts.DAL.models;

namespace accounts.core.Dto
{
    public class AccountDepositDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
    }


}
