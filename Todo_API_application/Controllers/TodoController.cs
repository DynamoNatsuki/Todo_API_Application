using Microsoft.AspNetCore.Mvc;
using Todo_API_application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo_API_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // GET: api/<TodoController>
        [HttpGet]
        public List<Todo> Get()
        {
            var db = new DBConnection();
            var todos = db.GetAllTodos();

            return todos;
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var db = new DBConnection();
            var todo = db.GetTodoById(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] Todo todo)
        {
            var db = new DBConnection();
            db.SaveTodo(todo);
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Todo todo)
        {
            var db = new DBConnection();
            db.UpdateTodoById(id, todo);
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var db = new DBConnection();
            db.DeleteTodoById(id);
        }
    }
}
