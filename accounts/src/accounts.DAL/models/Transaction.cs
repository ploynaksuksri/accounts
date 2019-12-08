using accounts.DAL.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace accounts.DAL.models
{
    [Table("Transactions")]
    public class Transaction : BaseEntity<int>, IAudited
    {
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [ForeignKey("AssociatedAccount")]
        public int AssociatedAccountId { get; set; }
        public Account AssociatedAccount { get; set; }

        public decimal OriginalAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal NetAmount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }

        public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }

    }

  

   
}
