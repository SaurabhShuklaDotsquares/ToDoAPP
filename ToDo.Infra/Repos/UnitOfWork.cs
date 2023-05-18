using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Interface;
using ToDo.Infra.Entities;

namespace ToDo.Infra.Repos
{
   public class UnitOfWork: IUnitOfWork
    {
        private readonly ToDoDbContext _toDoDbContext;

        private IUserRepository _userRepo;
        private IToDoRepository _toDoRepo;
        public UnitOfWork(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;

        }

        public IUserRepository User
        {
            get
            {
                if (_userRepo==null)
                {
                    _userRepo = new UserRepository(_toDoDbContext);
                }
                return _userRepo;
            }

           
        }
        public IToDoRepository ToDo
        {
            get
            {
                if (_toDoRepo == null)
                {
                    _toDoRepo = new ToDoRepository(_toDoDbContext);
                }
                return _toDoRepo;
            }
        }

        void IUnitOfWork.Save()
        {
            _toDoDbContext.SaveChanges();
        }

    }
}
