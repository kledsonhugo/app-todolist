using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace TodoListApi.WebTests;

public class WebInterfaceApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public WebInterfaceApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task Should_Load_Web_Interface_Page()
    {
        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Todo List", content);
        Assert.Contains("Nova Tarefa", content);
        Assert.Contains("Adicionar Tarefa", content);
    }

    [Fact]
    public async Task Should_Get_All_Todos()
    {
        // Act
        var response = await _client.GetAsync("/todos");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var todos = JsonSerializer.Deserialize<JsonElement[]>(content, _jsonOptions);
        Assert.NotNull(todos);
    }

    [Fact]
    public async Task Should_Create_New_Todo()
    {
        // Arrange
        var newTodo = new
        {
            title = "Test Task",
            description = "Test Description",
            priority = 2
        };
        var json = JsonSerializer.Serialize(newTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/todos", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdTodo = JsonSerializer.Deserialize<JsonElement>(responseContent, _jsonOptions);
        Assert.Equal("Test Task", createdTodo.GetProperty("title").GetString());
        Assert.Equal("Test Description", createdTodo.GetProperty("description").GetString());
        Assert.Equal(2, createdTodo.GetProperty("priority").GetInt32());
    }

    [Fact]
    public async Task Should_Filter_Todos_By_Completed_Status()
    {
        // Act - Get pending todos
        var pendingResponse = await _client.GetAsync("/todos?completed=false");
        pendingResponse.EnsureSuccessStatusCode();

        // Act - Get completed todos
        var completedResponse = await _client.GetAsync("/todos?completed=true");
        completedResponse.EnsureSuccessStatusCode();

        // Assert
        var pendingContent = await pendingResponse.Content.ReadAsStringAsync();
        var completedContent = await completedResponse.Content.ReadAsStringAsync();
        
        var pendingTodos = JsonSerializer.Deserialize<JsonElement[]>(pendingContent, _jsonOptions);
        var completedTodos = JsonSerializer.Deserialize<JsonElement[]>(completedContent, _jsonOptions);
        
        Assert.NotNull(pendingTodos);
        Assert.NotNull(completedTodos);
    }

    [Fact]
    public async Task Should_Complete_And_Uncomplete_Todo()
    {
        // Arrange - Create a new todo first
        var newTodo = new
        {
            title = "Test Complete Task",
            description = "Test Description",
            priority = 1
        };
        var json = JsonSerializer.Serialize(newTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/todos", content);
        createResponse.EnsureSuccessStatusCode();
        
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        var createdTodo = JsonSerializer.Deserialize<JsonElement>(responseContent, _jsonOptions);
        var todoId = createdTodo.GetProperty("id").GetInt32();

        // Act - Complete the todo
        var completeResponse = await _client.PostAsync($"/todos/{todoId}/complete", null);
        completeResponse.EnsureSuccessStatusCode();

        // Verify it's completed
        var getResponse = await _client.GetAsync($"/todos/{todoId}");
        getResponse.EnsureSuccessStatusCode();
        var todoContent = await getResponse.Content.ReadAsStringAsync();
        var todo = JsonSerializer.Deserialize<JsonElement>(todoContent, _jsonOptions);
        Assert.True(todo.GetProperty("isCompleted").GetBoolean());

        // Act - Uncomplete the todo
        var uncompleteResponse = await _client.PostAsync($"/todos/{todoId}/uncomplete", null);
        uncompleteResponse.EnsureSuccessStatusCode();

        // Verify it's not completed
        var getResponse2 = await _client.GetAsync($"/todos/{todoId}");
        getResponse2.EnsureSuccessStatusCode();
        var todoContent2 = await getResponse2.Content.ReadAsStringAsync();
        var todo2 = JsonSerializer.Deserialize<JsonElement>(todoContent2, _jsonOptions);
        Assert.False(todo2.GetProperty("isCompleted").GetBoolean());
    }

    [Fact]
    public async Task Should_Update_Todo()
    {
        // Arrange - Create a new todo first
        var newTodo = new
        {
            title = "Original Title",
            description = "Original Description",
            priority = 1
        };
        var json = JsonSerializer.Serialize(newTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/todos", content);
        createResponse.EnsureSuccessStatusCode();
        
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        var createdTodo = JsonSerializer.Deserialize<JsonElement>(responseContent, _jsonOptions);
        var todoId = createdTodo.GetProperty("id").GetInt32();

        // Act - Update the todo
        var updatedTodo = new
        {
            title = "Updated Title",
            description = "Updated Description",
            priority = 3
        };
        var updateJson = JsonSerializer.Serialize(updatedTodo, _jsonOptions);
        var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
        var updateResponse = await _client.PutAsync($"/todos/{todoId}", updateContent);
        updateResponse.EnsureSuccessStatusCode();

        // Verify the update
        var getResponse = await _client.GetAsync($"/todos/{todoId}");
        getResponse.EnsureSuccessStatusCode();
        var todoContent = await getResponse.Content.ReadAsStringAsync();
        var todo = JsonSerializer.Deserialize<JsonElement>(todoContent, _jsonOptions);
        
        Assert.Equal("Updated Title", todo.GetProperty("title").GetString());
        Assert.Equal("Updated Description", todo.GetProperty("description").GetString());
        Assert.Equal(3, todo.GetProperty("priority").GetInt32());
    }

    [Fact]
    public async Task Should_Delete_Todo()
    {
        // Arrange - Create a new todo first
        var newTodo = new
        {
            title = "To Be Deleted",
            description = "This will be deleted",
            priority = 1
        };
        var json = JsonSerializer.Serialize(newTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/todos", content);
        createResponse.EnsureSuccessStatusCode();
        
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        var createdTodo = JsonSerializer.Deserialize<JsonElement>(responseContent, _jsonOptions);
        var todoId = createdTodo.GetProperty("id").GetInt32();

        // Act - Delete the todo
        var deleteResponse = await _client.DeleteAsync($"/todos/{todoId}");
        deleteResponse.EnsureSuccessStatusCode();

        // Verify it's deleted (should return 404)
        var getResponse = await _client.GetAsync($"/todos/{todoId}");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Should_Validate_Required_Fields_When_Creating_Todo()
    {
        // Arrange - Todo without required title
        var invalidTodo = new
        {
            description = "Description without title",
            priority = 2
        };
        var json = JsonSerializer.Serialize(invalidTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/todos", content);

        // Assert - Should return bad request
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Handle_Invalid_Priority_Values()
    {
        // Arrange - Todo with invalid priority
        var invalidTodo = new
        {
            title = "Test Task",
            description = "Test Description",
            priority = 10 // Invalid priority value
        };
        var json = JsonSerializer.Serialize(invalidTodo, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/todos", content);

        // Assert - The API currently accepts any priority value, so it should return OK
        // This test verifies the current behavior
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdTodo = JsonSerializer.Deserialize<JsonElement>(responseContent, _jsonOptions);
        Assert.Equal(10, createdTodo.GetProperty("priority").GetInt32());
    }
}