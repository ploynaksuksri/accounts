using accounts.DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.models
{
    public class Transaction : BaseEntity<int>, IAudited
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public decimal OriginalAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal NetAmount { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }
        public int AssociatedAccountId { get; set; }
        public Account AssociatedAccount { get; set; }

        public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }

    }

  

   
}
