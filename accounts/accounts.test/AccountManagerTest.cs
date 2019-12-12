using accounts.core;
using accounts.core.Dto;
using accounts.DAL;
using accounts.DAL.models;
using accounts.DAL.repositories;
using Microsoft.EntityFrameworkCore;
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
       
        public AccountManagerTest()
        {

            var options = new DbContextOptionsBuilder<AccountDbContext>()
              .UseInMemoryDatabase(databaseName: "Accounts")
              .Options;
            var context = new AccountDbContext(options);

            _feeCalculator = new DefaultFeeCalculator();
            _accountRepo = new AccountRepository(context);
            _transactionRepo = new Repository<Transaction>(context);
            _accountManager = new AccountManager(_accountRepo, new TransactionManager(_feeCalculator, _transactionRepo));
           
        }

        [Fact]
        public void CreateAccount()
        {
            var accountNo = "NL61INGB3175229417";
            var account = new Account()
            {
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
            var accountNo = "NL69RABO9966705163";
            var account = new Account()
            {
                AccountNo = accountNo,
                Balance = 1000m
            };
            _accountRepo.Add(account);

            var depositInfo = new AccountDepositDto
            {
                AccountNo = accountNo,
                Amount = 1000m,             
            };

            var result = _accountManager.Deposit(depositInfo);
            var expectedBalance = 1999m;
            Assert.Equal(expectedBalance, result.Balance);
        }

        [Fact]
        public void Transfer()
        {
            var fromAccountNo = "NL30INGB5393795351";
            var toAccountNo = "NL27ABNA4458972219";
            var transferAmount = 500;
            var expectedBalanceFromAccount = 499m;
            var expectedBalanceToAccount = 999.5m;

            _accountRepo.Add(new Account() { AccountNo = fromAccountNo });
            _accountManager.Deposit(new AccountDepositDto { AccountNo = fromAccountNo, Amount = 1000 });

            _accountRepo.Add(new Account() { AccountNo = toAccountNo });

            _accountManager.Deposit(new AccountDepositDto { AccountNo = toAccountNo, Amount = 500 });
            var transferInfo = new AccountTransferDto
            {
                FromAccountNo = fromAccountNo,
                ToAccountNo = toAccountNo,
                Amount = transferAmount
            };

            var fromAccountResult = _accountManager.Transfer(transferInfo);
            Assert.Equal(expectedBalanceFromAccount, fromAccountResult.Balance);

            var toAccountResult = _accountRepo.Get(toAccountNo);
            Assert.Equal(expectedBalanceToAccount, toAccountResult.Balance);
        }

        [Fact]
        public void Transfer_should_fail_insuffient_balance()
        {
            var fromAccountNo = "NL77INGB8057339330";
            var toAccountNo = "NL12RABO5804762982";
            var transferAmount = 500;

            _accountRepo.Add(new Account() { AccountNo = fromAccountNo });
            _accountManager.Deposit(new AccountDepositDto { AccountNo = fromAccountNo, Amount = 200 });

            _accountRepo.Add(new Account() { AccountNo = toAccountNo });
            _accountManager.Deposit(new AccountDepositDto { AccountNo = toAccountNo, Amount = 500 });

            var transferInfo = new AccountTransferDto
            {
                FromAccountNo = fromAccountNo,
                ToAccountNo = toAccountNo,
                Amount = transferAmount
            };

            var ex = Assert.Throws<Exception>(() => _accountManager.Transfer(transferInfo));
            Assert.Equal("Account doesn't have sufficient balance.", ex.Message);
        }
    }
}
