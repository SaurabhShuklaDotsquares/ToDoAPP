using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Interface;
using ToDo.Infra.Entities;

namespace ToDo.Infra.Repos
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly ToDoDbContext _toDoDbContext;
        public RepositoryBase(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }


        public void Create(T entity)
        {
            _toDoDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _toDoDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _toDoDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _toDoDbContext.Set<T>().Where(condition).AsNoTracking();
        }

        public void Update(T entity)
        {
            _toDoDbContext.Set<T>().Update(entity);
        }

      
    }
}
