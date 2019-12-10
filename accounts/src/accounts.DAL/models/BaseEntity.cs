using System.ComponentModel.DataAnnotations;

namespace accounts.DAL.models
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
