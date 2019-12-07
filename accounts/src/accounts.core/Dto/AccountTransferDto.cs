using accounts.DAL.models;

namespace accounts.core.Dto
{
    public class AccountTransferDto
    {
        public Account FromAccount { get; set; }
        public decimal Amount { get; set; }
        public Account ToAccount { get; set; }
    }
}
