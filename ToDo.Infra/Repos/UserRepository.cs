using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Entites;
using ToDo.Core.Interface;
using ToDo.Infra.Entities;

namespace ToDo.Infra.Repos
{
   

    public class UserRepository : RepositoryBase<Core.Entites.User>, IUserRepository
    {

        public UserRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {

        }

        public void CreateUser(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.ModifiedAt = DateTime.UtcNow;
            Create(user);
        }
        public User GetUserByEmailandPwd(string email, string password)
        {
            return FindByCondition(t => t.Email == email && t.Password == password).FirstOrDefault();
        }

      
    }
}
