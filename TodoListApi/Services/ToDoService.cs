using TodoListApi.Models;
using TodoListApi.DTOs;

namespace TodoListApi.Services;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<ToDoItem> CreateAsync(CreateToDoItemDto createDto);
    Task<ToDoItem?> UpdateAsync(int id, UpdateToDoItemDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ToDoItem>> GetByStatusAsync(bool isCompleted);
}

public class ToDoService : IToDoService
{
    private readonly List<ToDoItem> _todos = new();
    private int _nextId = 1;

    public ToDoService()
    {
        // Adicionar tarefas de exemplo
        SeedSampleTasks();
    }

    private void SeedSampleTasks()
    {
        var sampleTasks = new[]
        {
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Estudar ASP.NET Core",
                Description = "Aprender sobre APIs mínimas e Swagger",
                Priority = 3,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Fazer compras no supermercado",
                Description = "Comprar ingredientes para o jantar: arroz, feijão, carne e verduras",
                Priority = 2,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Exercitar-se",
                Description = "Ir à academia ou fazer uma caminhada de 30 minutos",
                Priority = 2,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                CompletedAt = DateTime.UtcNow.AddDays(-3).AddHours(2)
            },
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Ler documentação do .NET",
                Description = "Ler sobre novidades do .NET 8 e Entity Framework",
                Priority = 1,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddHours(-5)
            },
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Reunião de equipe",
                Description = "Daily standup meeting às 9h - revisar progresso da sprint",
                Priority = 3,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                CompletedAt = DateTime.UtcNow.AddDays(-1).AddHours(1)
            },
            new ToDoItem
            {
                Id = _nextId++,
                Title = "Organizar área de trabalho",
                Description = "Limpar mesa, organizar cabos e documentos",
                Priority = 1,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            }
        };

        _todos.AddRange(sampleTasks);
    }

    public Task<IEnumerable<ToDoItem>> GetAllAsync()
    {
        return Task.FromResult(_todos.AsEnumerable());
    }

    public Task<ToDoItem?> GetByIdAsync(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(todo);
    }

    public Task<ToDoItem> CreateAsync(CreateToDoItemDto createDto)
    {
        var todo = new ToDoItem
        {
            Id = _nextId++,
            Title = createDto.Title,
            Description = createDto.Description,
            Priority = createDto.Priority,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _todos.Add(todo);
        return Task.FromResult(todo);
    }

    public Task<ToDoItem?> UpdateAsync(int id, UpdateToDoItemDto updateDto)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return Task.FromResult<ToDoItem?>(null);

        if (!string.IsNullOrWhiteSpace(updateDto.Title))
            todo.Title = updateDto.Title;

        if (updateDto.Description != null)
            todo.Description = updateDto.Description;

        if (updateDto.Priority.HasValue)
            todo.Priority = updateDto.Priority.Value;

        if (updateDto.IsCompleted.HasValue)
        {
            todo.IsCompleted = updateDto.IsCompleted.Value;
            todo.CompletedAt = updateDto.IsCompleted.Value ? DateTime.UtcNow : null;
        }

        return Task.FromResult<ToDoItem?>(todo);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return Task.FromResult(false);

        _todos.Remove(todo);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<ToDoItem>> GetByStatusAsync(bool isCompleted)
    {
        var filteredTodos = _todos.Where(t => t.IsCompleted == isCompleted);
        return Task.FromResult(filteredTodos);
    }
}
