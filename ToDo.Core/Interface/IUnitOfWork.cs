using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Core.Interface
{
    public interface IUnitOfWork
    {
         IUserRepository User { get;  }
         IToDoRepository ToDo { get;  }

        void Save();
    }
}
