using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.WebAPI.DTO
{
    public class ToDoForCreateUpdateDto
    {

        [Required]
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}
