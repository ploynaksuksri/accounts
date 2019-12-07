namespace accounts.core
{
    public interface IFeeCalculator
    {
        decimal CalculateDepositFee(decimal amount);
    }
}
