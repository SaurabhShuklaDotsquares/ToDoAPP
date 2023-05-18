using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Helpers;
using ToDo.Core.Specefication;

namespace ToDo.Core.Interface
{
    public interface IToDoRepository:IRepositoryBase<Core.Entites.ToDos>
    {


        PageList<Entites.ToDos> GetToDos(ToDoSearchSpacs space);
        List<Entites.ToDos> GetAllToDoc();

        PageList<Entites.ToDos> GetToDosByUser(int userId, ToDoSearchSpacs specs);


        Entites.ToDos GetToDoById(int id);

        void CreateToDo(Entites.ToDos todo);

        void UpdateToDo(Entites.ToDos todo);


        void DeleteToDo(Entites.ToDos todo);


    }
}
