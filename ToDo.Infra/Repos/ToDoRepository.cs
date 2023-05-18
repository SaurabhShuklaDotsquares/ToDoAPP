using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Entites;
using ToDo.Core.Helpers;
using ToDo.Core.Interface;
using ToDo.Core.Specefication;
using ToDo.Infra.Entities;

namespace ToDo.Infra.Repos
{
    public class ToDoRepository : RepositoryBase<Core.Entites.ToDos>, IToDoRepository
    {

        public ToDoRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {

        }

        public void CreateToDo(ToDos todo)
        {
            todo.Status = 1;
            todo.CreatedAt = DateTime.Now;
            todo.ModifiedAt = DateTime.Now;
            Create(todo);
            
        }

        public void UpdateToDo(ToDos todo)
        {
            todo.ModifiedAt = DateTime.Now;

            Update(todo);
        }

        public void DeleteToDo(ToDos todo)
        {
            Delete(todo);
        }

        public List<ToDos> GetAllToDoc()
        {
            return FindAll().ToList();
        }

        public ToDos GetToDoById(int id)
        {
            return FindByCondition(x=>x.Id==id).SingleOrDefault();
        }

        

       
        public PageList<ToDos> GetToDos(ToDoSearchSpacs specs)
        {
            var todos = FindAll();

            if (specs.Status > 0)
            {
                todos = FindByCondition(t => t.Status == specs.Status);
            }

            if (!string.IsNullOrWhiteSpace(specs.Title))
            {
                todos = todos.Where(t => t.Title.ToLower().Contains(specs.Title.ToLower()));
            }

            todos = SortHelper<ToDos>.ApplySort(todos, specs.OrderBy);

            return PageList<ToDos>.ToPageList(todos, specs.PageNumber, specs.PageSize);
        }

        public PageList<ToDos> GetToDosByUser(int userId, ToDoSearchSpacs specs)
        {
            var todos = FindByCondition(t => t.UserId == userId);

            if (specs.Status > 0)
            {
                todos = FindByCondition(t => t.Status == specs.Status);
            }

            if (!string.IsNullOrWhiteSpace(specs.Title))
            {
                todos = todos.Where(t => t.Title.ToLower().Contains(specs.Title.ToLower()));
            }

            todos = SortHelper<ToDos>.ApplySort(todos, specs.OrderBy);

            return PageList<ToDos>.ToPageList(todos, specs.PageNumber, specs.PageSize);
        }


    }
}
