using System;

namespace ToDo.WebAPI.DTO
{
    public class ToDoDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte? Status { get; set; }
        public DateTime? Duedate { get; set; }
    }
}
