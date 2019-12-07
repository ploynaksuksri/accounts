using System;
using System.Collections.Generic;
using System.Text;
using accounts.DAL.models;
using accounts.core.Dto;

namespace accounts.core
{
    public interface IAccountManager
    {
        Account CreateAccount(Account account);
        Account Deposit(AccountDepositDto depositDto);
        Account Transfer(AccountTransferDto transferDto);
    }
}
