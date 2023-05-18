using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Core.Specefication
{
    public class ToDoSearchSpacs: GenericSearchSpecs
    {
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
