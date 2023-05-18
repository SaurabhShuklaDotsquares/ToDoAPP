using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Core.Entites
{
    public partial class User
    {
        public User()
        {
            ToDos = new HashSet<ToDos>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<ToDos> ToDos { get; set; }
        public string Photo { get; set; }


    }
}
