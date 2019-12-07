using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.models
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
