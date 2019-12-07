using accounts.DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.models
{
    public class Customer : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
