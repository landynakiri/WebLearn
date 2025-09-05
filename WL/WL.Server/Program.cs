
using Microsoft.EntityFrameworkCore;
using WL.Server.RestfulPractice.P01;

namespace WL.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.MapGet("/hello", () => "Hello World!");

            var todoItems = app.MapGroup("/todoitems");
            todoItems.MapGet("/", GetAllTodos);
            todoItems.MapGet("/complete", GetCompleteTodos);
            todoItems.MapGet("/{id}", GetTodo);
            todoItems.MapPost("/", CreateTodo);
            todoItems.MapPut("/{id}", UpdateTodo);
            todoItems.MapDelete("/{id}", DeleteTodo);

            app.Run();
        }

        private static async Task<IResult> GetAllTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Select(x=>new TodoItemDTO(x)).ToArrayAsync());
        }

        private static async Task<IResult> GetCompleteTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Where(t =>  t.IsComplete).Select(x=>new TodoItemDTO(x)).ToListAsync());
        }

        private static async Task<IResult> GetTodo(int id, TodoDb db)
        {
            return await db.Todos.FindAsync(id)
                is Todo todo
                    ? TypedResults.Ok(new TodoItemDTO(todo))
                    : TypedResults.NotFound();
        }

        private static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDb db)
        {
            var todoItem = new Todo
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };
            db.Todos.Add(todoItem);
            await db.SaveChangesAsync();

            todoItemDTO = new TodoItemDTO(todoItem);

            return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
        }

        private static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, TodoDb db)
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return TypedResults.NotFound();

            todo.Name = todoItemDTO.Name;
            todo.IsComplete = todoItemDTO.IsComplete;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private static async Task<IResult> DeleteTodo(int id, TodoDb db)
        {
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
