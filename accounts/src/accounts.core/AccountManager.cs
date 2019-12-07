using accounts.DAL.models;
using accounts.DAL.repositories;
using accounts.core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.core
{
    public class AccountManager
    {
        private IRepository<Account> _accountRepo;
        private IRepository<Transaction> _transactionRepo;
        private IFeeCalculator _feeCalculator;

        public AccountManager(IFeeCalculator feeCalculator,
            IRepository<Account> accountRepo,
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

        public Account Deposit(AccountDepositDto depositInfo)
        {
            Transaction transaction = new Transaction()
            {
                TransactionType = TransactionTypeEnum.Deposit,
                AccountId = depositInfo.ToAccount.Id,
                OriginalAmount = depositInfo.Amount,
                Fee = _feeCalculator.CalculateDepositFee(depositInfo.Amount)
            };
            transaction.NetAmount = transaction.OriginalAmount - transaction.Fee;
            _transactionRepo.Add(transaction);

            depositInfo.ToAccount.Balance += transaction.NetAmount;
            _accountRepo.Update(depositInfo.ToAccount);
            return depositInfo.ToAccount;
        }

        public Account Transfer(AccountTransferDto transferInfo)
        {
            // Add transfer record for FromAccount
            transferInfo.FromAccount.Balance -= transferInfo.Amount;
            _accountRepo.Update(transferInfo.FromAccount);
            _transactionRepo.Add(new Transaction
            {
                TransactionType = TransactionTypeEnum.Transfer,
                AccountId = transferInfo.FromAccount.Id,
                AssociatedAccountId = transferInfo.ToAccount.Id,
                OriginalAmount = transferInfo.Amount,
                NetAmount = transferInfo.Amount,
                Fee = 0.0m
            });

            // Add received record for ToAccount
            transferInfo.ToAccount.Balance += transferInfo.Amount;
            _accountRepo.Update(transferInfo.ToAccount);
            _transactionRepo.Add(new Transaction
            {
                TransactionType = TransactionTypeEnum.Received,
                AccountId = transferInfo.ToAccount.Id,
                AssociatedAccountId = transferInfo.FromAccount.Id,
                OriginalAmount = transferInfo.Amount,
                NetAmount = transferInfo.Amount,
                Fee = 0.0m
            });
            return transferInfo.FromAccount;
        }
    }

   
   
}
