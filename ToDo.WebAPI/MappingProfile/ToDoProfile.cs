using AutoMapper;
using ToDo.Core.Entites;
using ToDo.WebAPI.DTO;

namespace ToDo.WebAPI.MappingProfile
{
    public class ToDoProfile:Profile
    {
        public ToDoProfile()
        {
            CreateMap<Core.Entites.ToDos, ToDoDto>();

            CreateMap<ToDoForCreateUpdateDto, ToDos>();
        }
    }
}
