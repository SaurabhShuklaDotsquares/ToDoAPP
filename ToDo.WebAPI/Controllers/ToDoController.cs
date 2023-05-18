using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ToDo.Core.Interface;
using ToDo.Core.Specefication;
using ToDo.WebAPI.DTO;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ToDoController(ILogger<ToDoController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
      
        public IActionResult GetAllToDocs([FromQuery] ToDoSearchSpacs space)
        {
            try
            {
                var todo = _unitOfWork.ToDo.GetToDos(space);
                //var todo = _unitOfWork.ToDo.GetAllToDoc();
                _logger.LogInformation("Retruned all todo from database");

                var pageMetaData = new
                {
                    todo.CurrentPage,
                    todo.TotalPage,
                    todo.TotalCount,
                    todo.HasNext,
                    todo.HasPrevious,
                };

                //Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(pageMetaData));

               Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pageMetaData));

                var todosDto = _mapper.Map<List<ToDoDto>>(todo);

               return Ok(todosDto);
                
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside GetalltoDos action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internasl server Error");
            }
        }

        [HttpGet("{id}", Name = "GetToDoById")]
        public IActionResult GetToDoById(int id)
        {
            try
            {
                var todo = _unitOfWork.ToDo.GetToDoById(id);

                if (todo == null)
                {
                    return NotFound();
                }

                var todosDto = _mapper.Map<ToDoDto>(todo);

                _logger.LogInformation("Retruned all todo from database");
                return Ok(todosDto);
                // return JsonResult(todosDto);
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside GetalltoDos action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internasl server Error");
            }
        }



        [HttpPost]
        public IActionResult CreateToDo(ToDoForCreateUpdateDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("The client's has provided invalid data");
                    return BadRequest();
                }
                var toDoEntity = _mapper.Map<Core.Entites.ToDos>(model);

                 toDoEntity.UserId = 1;
                _unitOfWork.ToDo.CreateToDo(toDoEntity);
                _unitOfWork.Save();

                var toDoDto = _mapper.Map<ToDoDto>(toDoEntity);

                return CreatedAtRoute("GetToDoById", new { id = toDoEntity.Id }, toDoDto);
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside CreateToDo action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internal server Error");
            }




        }


        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, ToDoForCreateUpdateDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("The client's has provided invalid data");
                    return BadRequest();
                }
                var toDoEntity = _unitOfWork.ToDo.GetToDoById(id);
                if (toDoEntity==null)
                {
                    _logger.LogError($"To Do with {id} hasn't been found in db");
                    return NotFound();
                }

                _mapper.Map(model, toDoEntity);

                _unitOfWork.ToDo.Update(toDoEntity);

                _unitOfWork.Save();

                var updateToDo = _mapper.Map<ToDoDto>(toDoEntity);
                return Ok(updateToDo);
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside UpdateToDo action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internal server Error");
            }




        }



        [HttpPatch("{id}")]

        
        public IActionResult CompleteToDo(int id, ToDoForCreateUpdateDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("The client's has provided invalid data");
                    return BadRequest();
                }
                var toDoEntity = _unitOfWork.ToDo.GetToDoById(id);
                if (toDoEntity == null)
                {
                    _logger.LogError($"To Do with {id} hasn't been found in db");
                    return NotFound();
                }
                toDoEntity.Status = 2;

                _mapper.Map(model, toDoEntity);

                _unitOfWork.ToDo.Update(toDoEntity);

                _unitOfWork.Save();

                var updateToDo = _mapper.Map<ToDoDto>(toDoEntity);
                return Ok(updateToDo);
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside CompleteToDo action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internal server Error");
            }




        }



        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("The client's has provided invalid data");
                    return BadRequest();
                }
                var toDoEntity = _unitOfWork.ToDo.GetToDoById(id);
                if (toDoEntity == null)
                {
                    _logger.LogError($"To Do with {id} hasn't been found in db");
                    return NotFound();
                }
               

                _unitOfWork.ToDo.Delete(toDoEntity);
                _unitOfWork.Save();

                var updateToDo = _mapper.Map<ToDoDto>(toDoEntity);
                return Ok("Todo has been deletd successfully");
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong inside DeleteToDo action :{ex.GetBaseException().Message}"); ;

                return StatusCode(500, "Internal server Error");
            }




        }

    }
}





