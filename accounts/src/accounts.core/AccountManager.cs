using accounts.DAL.models;
using accounts.DAL.repositories;
using accounts.core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.core
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository _accountRepo;
        private IRepository<Transaction> _transactionRepo;
        private IFeeCalculator _feeCalculator;

        public AccountManager(IFeeCalculator feeCalculator,
            IAccountRepository accountRepo,
            IRepository<Transaction> transactionRepo)
        {
            _feeCalculator = feeCalculator;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepo.Add(account);
        }

        public Account Deposit(AccountDepositDto depositDto)
        {
            Transaction transaction = new Transaction()
            {
                TransactionType = TransactionTypeEnum.Deposit,
                AccountId = depositDto.ToAccount.Id,
                OriginalAmount = depositDto.Amount,
                Fee = _feeCalculator.CalculateDepositFee(depositDto.Amount)
            };
            transaction.NetAmount = transaction.OriginalAmount - transaction.Fee;
            _transactionRepo.Add(transaction);

            depositDto.ToAccount.Balance += transaction.NetAmount;
            _accountRepo.Update(depositDto.ToAccount);
            return depositDto.ToAccount;
        }

        public Account Transfer(AccountTransferDto transferDto)
        {
            // Add transfer record for FromAccount
            transferDto.FromAccount.Balance -= transferDto.Amount;
            _accountRepo.Update(transferDto.FromAccount);
            _transactionRepo.Add(new Transaction
            {
                TransactionType = TransactionTypeEnum.Transfer,
                AccountId = transferDto.FromAccount.Id,
                AssociatedAccountId = transferDto.ToAccount.Id,
                OriginalAmount = transferDto.Amount,
                NetAmount = transferDto.Amount,
                Fee = 0.0m
            });

            // Add received record for ToAccount
            transferDto.ToAccount.Balance += transferDto.Amount;
            _accountRepo.Update(transferDto.ToAccount);
            _transactionRepo.Add(new Transaction
            {
                TransactionType = TransactionTypeEnum.Received,
                AccountId = transferDto.ToAccount.Id,
                AssociatedAccountId = transferDto.FromAccount.Id,
                OriginalAmount = transferDto.Amount,
                NetAmount = transferDto.Amount,
                Fee = 0.0m
            });
            return transferDto.FromAccount;
        }
    }

   
   
}
