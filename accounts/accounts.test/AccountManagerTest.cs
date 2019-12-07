using accounts.core;
using accounts.core.Dto;
using accounts.DAL;
using accounts.DAL.models;
using accounts.DAL.repositories;
using System;
using Xunit;

namespace accounts.test
{
    public class AccountManagerTest
    {
        private AccountManager _accountManager;
        private IFeeCalculator _feeCalculator;
        private IAccountRepository _accountRepo;
        private IRepository<Transaction> _transactionRepo;
        private ITransactionManager _transactionManager;

        public AccountManagerTest()
        {
            _feeCalculator = new DefaultFeeCalculator();
            _accountRepo = new AccountRepository();
            _transactionRepo = new Repository<Transaction>();
            _accountManager = new AccountManager(_accountRepo, new TransactionManager(_feeCalculator, _transactionRepo));
           
        }

        [Fact]
        public void CreateAccount()
        {
            var accountNo = "NL61INGB3175229417";
            var account = new Account()
            {
                Id = 1,
                AccountNo = accountNo,
                Balance = 0.0m
            };

            _accountRepo.Add(account);

            var result = _accountRepo.Get(accountNo);
            Assert.NotNull(result);
            Assert.Equal(accountNo, result.AccountNo);
        }

        [Fact]
        public void Deposit()
        {
            var accountNo = "NL61INGB3175229417";
            var account = new Account()
            {
                Id = 1,
                AccountNo = accountNo,
                Balance = 1000m
            };
            _accountRepo.Add(account);

            var depositInfo = new AccountDepositDto
            {
                AccountId = 2,
                Amount = 1000m,
                TransactionType = TransactionTypeEnum.Deposit
            };

            var result = _accountManager.Deposit(depositInfo);

            var expectedBalance = 1999m;
            Assert.Equal(expectedBalance, result.Balance);


        }
    }
}
