using accounts.DAL.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace accounts.DAL.repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {

        public AccountRepository(AccountDbContext dbContext) : base(dbContext)
        {

        }

        public Account Get(string accountNo)
        {
            return _dbContext.Accounts.FirstOrDefault(e => e.AccountNo == accountNo && e.IsDeleted == false);
        }

        public bool IsAccountNoAvailable(string accountNo)
        {
            var account = Get(accountNo);
            return account == null;
        }

        public override void Update(Account obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }

    public interface IAccountRepository : IRepository<Account>
    {
        Account Get(string accountNo);
        bool IsAccountNoAvailable(string accountNo);
    }
}
