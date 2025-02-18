﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accounts.DAL.repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity: class
    {
        protected List<TEntity> _list = new List<TEntity>();
        protected AccountDbContext _dbContext;

        public Repository(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return _dbContext.Set<TEntity>();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }

        TEntity IRepository<TEntity>.Add(TEntity obj)
        {
            _dbContext.Set<TEntity>().Add(obj);
            _dbContext.SaveChanges();
            return obj;
        }
    }
}
