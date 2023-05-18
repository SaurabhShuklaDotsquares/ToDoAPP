using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Core.Entites
{
    public partial class ToDos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte? Status { get; set; }
        public DateTime? Duedate { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual User User { get; set; }
    }

}
