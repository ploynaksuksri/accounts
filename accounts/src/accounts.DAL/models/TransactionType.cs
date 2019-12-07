using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.models
{
    public class TransactionType : BaseEntity<TransactionTypeEnum>
    {
        public string Name { get; set; }
    }

    public enum TransactionTypeEnum
    {
        Deposit = 1,
        Transfer = 2,
        Received = 3
    }
}
