using System;
using System.Collections.Generic;
using System.Text;

namespace accounts.DAL.interfaces
{
    public interface IAudited : ISoftDelete
    {
        DateTime CreationTime { get; set; }
        DateTime? ModificationTime { get; set; }
    }

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
