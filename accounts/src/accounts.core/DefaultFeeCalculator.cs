namespace accounts.core
{
    public class DefaultFeeCalculator : IFeeCalculator
    {
        public const decimal DepositFeePercent = 0.1m;
        public decimal CalculateDepositFee(decimal amount)
        {
            return (amount * DepositFeePercent) / 100;
        }
    }

}
