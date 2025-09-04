using TodoListApi.Services;
using TodoListApi.DTOs;
using TodoListApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IToDoService, ToDoService>();

// Adicionar CORS para permitir acesso da interface web
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors();

// Servir arquivos estáticos (HTML, CSS, JS)
app.UseStaticFiles();

app.UseHttpsRedirection();

// Rota para a página principal
app.MapGet("/", () => Results.Redirect("/index.html"))
.WithName("Home")
.WithSummary("Redirecionar para a interface web");

// Endpoints da API de To-Do List

// GET /todos - Listar todas as tarefas
app.MapGet("/todos", async (IToDoService todoService, bool? completed) =>
{
    if (completed.HasValue)
    {
        return Results.Ok(await todoService.GetByStatusAsync(completed.Value));
    }
    return Results.Ok(await todoService.GetAllAsync());
})
.WithName("GetTodos")
.WithOpenApi()
.WithSummary("Obter todas as tarefas ou filtrar por status");

// GET /todos/{id} - Obter uma tarefa específica
app.MapGet("/todos/{id:int}", async (int id, IToDoService todoService) =>
{
    var todo = await todoService.GetByIdAsync(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("GetTodoById")
.WithOpenApi()
.WithSummary("Obter uma tarefa por ID");

// POST /todos - Criar uma nova tarefa
app.MapPost("/todos", async (CreateToDoItemDto createDto, IToDoService todoService) =>
{
    if (string.IsNullOrWhiteSpace(createDto.Title))
    {
        return Results.BadRequest("O título da tarefa é obrigatório.");
    }

    var todo = await todoService.CreateAsync(createDto);
    return Results.Created($"/todos/{todo.Id}", todo);
})
.WithName("CreateTodo")
.WithOpenApi()
.WithSummary("Criar uma nova tarefa");

// PUT /todos/{id} - Atualizar uma tarefa
app.MapPut("/todos/{id:int}", async (int id, UpdateToDoItemDto updateDto, IToDoService todoService) =>
{
    var todo = await todoService.UpdateAsync(id, updateDto);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("UpdateTodo")
.WithOpenApi()
.WithSummary("Atualizar uma tarefa existente");

// DELETE /todos/{id} - Deletar uma tarefa
app.MapDelete("/todos/{id:int}", async (int id, IToDoService todoService) =>
{
    var deleted = await todoService.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteTodo")
.WithOpenApi()
.WithSummary("Deletar uma tarefa");

// POST /todos/{id}/complete - Marcar tarefa como concluída
app.MapPost("/todos/{id:int}/complete", async (int id, IToDoService todoService) =>
{
    var updateDto = new UpdateToDoItemDto { IsCompleted = true };
    var todo = await todoService.UpdateAsync(id, updateDto);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("CompleteTodo")
.WithOpenApi()
.WithSummary("Marcar uma tarefa como concluída");

// POST /todos/{id}/uncomplete - Marcar tarefa como não concluída
app.MapPost("/todos/{id:int}/uncomplete", async (int id, IToDoService todoService) =>
{
    var updateDto = new UpdateToDoItemDto { IsCompleted = false };
    var todo = await todoService.UpdateAsync(id, updateDto);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("UncompleteTodo")
.WithOpenApi()
.WithSummary("Marcar uma tarefa como não concluída");

app.Run();

// Make Program class accessible for testing
public partial class Program { }
