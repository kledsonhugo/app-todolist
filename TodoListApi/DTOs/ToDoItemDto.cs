namespace TodoListApi.DTOs;

public class CreateToDoItemDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Priority { get; set; } = 1;
}

public class UpdateToDoItemDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public int? Priority { get; set; }
}
