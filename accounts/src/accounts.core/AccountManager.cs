using accounts.core.Dto;
using accounts.DAL.models;
using accounts.DAL.repositories;
using System;
using System.Collections.Generic;

namespace accounts.core
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository _accountRepo;
        private ITransactionManager _transactionManager;

        public AccountManager(IAccountRepository accountRepo,
            ITransactionManager transactionManager)
        {
            _accountRepo = accountRepo;
            _transactionManager = transactionManager;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepo.GetAll();
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepo.Add(account);
        }

        public Account Deposit(AccountDepositDto depositDto)
        {
            var account = GetAccount(depositDto.AccountNo);
            depositDto.AccountId = account.Id;
            var transaction = _transactionManager.Deposit(depositDto);
            account.Balance += transaction.NetAmount;
            _accountRepo.Update(account);
            return account;
        }

        public Account Transfer(AccountTransferDto transferDto)
        {          
            if (!IsTransferable(transferDto.FromAccount.AccountNo, transferDto.Amount))
            {
                throw new Exception($"Account no. {transferDto.FromAccount.AccountNo}'s balance is insufficient.");
            }

            // Transfer
            _transactionManager.Transfer(transferDto);
            transferDto.FromAccount.Balance -= transferDto.Amount;
            _accountRepo.Update(transferDto.FromAccount);
            
            // Receive
            _transactionManager.Receive(transferDto);
            transferDto.ToAccount.Balance += transferDto.Amount;
            _accountRepo.Update(transferDto.ToAccount);

            return transferDto.FromAccount;
        }

        private Account GetAccount(int id)
        {
            var account = _accountRepo.Get(id);
            if (account == null)
            {
                throw new Exception("Account is not found.");
            }
            return account;
        }

        private Account GetAccount(string accountNo)
        {
            var account = _accountRepo.Get(accountNo);
            if (account == null)
            {
                throw new Exception("Account is not found.");
            }
            return account;
        }

        private bool IsTransferable(string accountNo, decimal amount)
        {
            var account = GetAccount(accountNo);

            return account.Balance >= amount;
            
        }
    }

   
   
}
