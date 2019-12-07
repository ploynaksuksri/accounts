using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accounts.DAL.repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity: class
    {
        private List<TEntity> _list = new List<TEntity>();

        public void Add(TEntity obj)
        {
            _list.Add(obj);
        }

        public void Deleted(TEntity obj)
        {
            _list.Remove(obj);
        }
        public void Delete(int id)
        {
            
        }

        public TEntity Get(int id)
        {
            return _list.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _list;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
