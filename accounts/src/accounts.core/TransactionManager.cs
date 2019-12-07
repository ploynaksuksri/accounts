using accounts.core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.core
{
    public class TransactionManager : ITransactionManager
    {
        private IFeeCalculator _feeCalculator;

        public TransactionManager(IFeeCalculator feeCalculator)
        {
            _feeCalculator = feeCalculator;
        }

        public void Deposit(AccountDepositDto depositDto)
        {
            
        }

        public void Transfer(AccountTransferDto transferDto)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITransactionManager
    {
        void Deposit(AccountDepositDto depositDto);
        void Transfer(AccountTransferDto transferDto);
    }
}
