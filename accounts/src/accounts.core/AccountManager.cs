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
            if (!IsAccountNoAvailable(account.AccountNo))
            {
                throw new Exception($"Account no. {account.AccountNo} is not available.");
            }
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
            if (!IsTransferable(transferDto))
            {
                throw new Exception($"Account doesn't have sufficient balance.");
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


        private Account GetAccount(string accountNo)
        {
            if (string.IsNullOrEmpty(accountNo))
            {
                throw new Exception("Account No is empty.");
            }

            var account = _accountRepo.Get(accountNo);
            if (account == null)
            {
                throw new Exception("Account is not found.");
            }
            return account;
        }


        private bool IsTransferable(AccountTransferDto transferDto)
        {
            if (transferDto.Amount <= 0)
                return false;

            transferDto.FromAccount = GetAccount(transferDto.FromAccountNo);
            transferDto.ToAccount = GetAccount(transferDto.ToAccountNo);
            return transferDto.FromAccount.Balance >= transferDto.Amount;
        }

        private bool IsAccountNoAvailable(string accountNo)
        {
            return _accountRepo.IsAccountNoAvailable(accountNo);
        }

    }



}
