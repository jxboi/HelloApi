using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloApi.Models;
using Microsoft.AspNetCore.Authorization;
using HelloApi.Repository;
using NuGet.Protocol.Core.Types;
using FluentValidation.Results;

namespace HelloApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemsController(IUnitOfWork unitOfWork, ILogger<TodoItem> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoList = await _unitOfWork.Todo.GetAllTodoItemsAsync();

            if (todoList == null)
            {
                return NotFound();
            }
                
            return Ok(todoList);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}", Name = "GetTodoItem")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _unitOfWork.Todo.GetTodoItemByIdAsync(id);
           
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("todoItem object is null");
            }

            if (id != todoItem.Id)
            {
                return BadRequest("todoItem Id in query string do not match id in body");
            }

            TodoItemValidator validator = new TodoItemValidator();
            ValidationResult result = validator.Validate(todoItem);

            if (!result.IsValid)
            {
                return BadRequest("Invalid todoItem object");
            }

            var todoItemFound = await _unitOfWork.Todo.GetTodoItemByIdAsync(id);
            if (todoItemFound == null)
            {
                return NotFound();
            }

            _unitOfWork.Todo.Update(todoItem);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("TodoItem object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid TodoItem object");
            }

            _unitOfWork.Todo.AddTodoItem(todoItem);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetTodoItem", new { todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _unitOfWork.Todo.GetTodoItemByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _unitOfWork.Todo.RemoveTodoItem(todoItem);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
