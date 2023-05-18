using ToDo.Core.Entites;

namespace ToDo.Core.Interface
{


    public interface IUserRepository : IRepositoryBase<Entites.User>
    {
        void CreateUser(User user);
        User GetUserByEmailandPwd(string email, string password);
    }
}
