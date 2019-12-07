using accounts.DAL.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace accounts.DAL.repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public Account Get(string accountNo)
        {
            return _list.Find(e => e.AccountNo == accountNo);
        }

        public override void Update(Account obj)
        {
            var account = Get(obj.AccountNo);
            _list.Remove(account);
            _list.Add(obj);
        }
    }

    public interface IAccountRepository : IRepository<Account>
    {
        Account Get(string accountNo);
    }
}
