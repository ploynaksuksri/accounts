using accounts.DAL.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace accounts.DAL.models
{
    [Table("Transactions")]
    public class Transaction : BaseEntity<int>, IAudited
    {
        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        [ForeignKey("AssociatedAccount")]
        public int? AssociatedAccountId { get; set; }
        public Account AssociatedAccount { get; set; }

        [Required]

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fee { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetAmount { get; set; }

        [Required]
        public TransactionTypeEnum TransactionType { get; set; }

        public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }

    }

  

   
}
