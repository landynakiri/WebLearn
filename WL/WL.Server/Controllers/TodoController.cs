using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WL.Server.Model;

namespace WL.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TodoController: ControllerBase
    {

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }
    }
}
