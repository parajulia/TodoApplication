using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.DataModel;
using TodoApplication.Entities;
using TodoApplication.Repository;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems() {
            var toDoItems = await _repository.FindAll();
            return Ok(toDoItems);
        
        }

        [HttpGet]
        [Route("{id}")]

        public  IActionResult GetById(int id)
        {
            var todoItem = _repository.GetById(id);
            var todoItemDTO = new TodoItemDTO
            {
                Id = todoItem.Id,
                IsComplete = todoItem.IsComplete,
                Title = todoItem.Title,
            };

            return Ok(todoItemDTO);
        }

        [HttpPost]
        public  IActionResult SaveItem([FromBody] TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Title = todoItemDTO.Title,
            };
         _repository.Create(todoItem);
          return CreatedAtAction(nameof(GetById), new {id = todoItem.Id}, todoItem);
        }

        [HttpPut]
        public  IActionResult UpdateItem([FromBody] TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Title = todoItemDTO.Title,
            };
            _repository.Update(todoItem);
            return Ok("update successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            _repository.Delete(id);
            return Ok("Item deleted Successfully");
        }
    }
}
