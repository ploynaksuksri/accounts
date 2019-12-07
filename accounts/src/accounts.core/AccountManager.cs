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
        private ITransactionManager _transactionManager;

        public AccountManager(IAccountRepository accountRepo,
            ITransactionManager transactionManager)
        {
            _accountRepo = accountRepo;
            _transactionManager = transactionManager;
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepo.Add(account);
        }

        public Account Deposit(AccountDepositDto depositDto)
        {
            var account = GetAccount(depositDto.AccountId);
            var transaction = _transactionManager.Deposit(depositDto);
            account.Balance += transaction.NetAmount;
            _accountRepo.Update(account);
            return account;
        }

        public Account Transfer(AccountTransferDto transferDto)
        {
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
    }

   
   
}
