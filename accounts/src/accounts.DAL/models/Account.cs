using accounts.DAL.interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounts.DAL.models
{
    [Table("Accounts")]
    public class Account : BaseEntity<int>, IAudited
    {
        [StringLength(18)]
        [Required]
        public string AccountNo { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}