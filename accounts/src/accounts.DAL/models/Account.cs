using accounts.DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.models
{
    public class Account: BaseEntity<int>, IAudited
    {
        public string AccountNo { get; set; }    
        public decimal Balance { get; set; }
     
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }

    }




}
