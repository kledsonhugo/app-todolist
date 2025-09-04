# Todo List API - GitHub Copilot Instructions

**ALWAYS reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Project Overview
Todo List API is a .NET 8 ASP.NET Core web application that provides a RESTful API for managing todo tasks. It features minimal APIs architecture, in-memory data storage, Swagger documentation, and a modern web interface.

## Working Effectively

### Prerequisites and Setup
- **.NET 8 SDK**: Required. Available version 8.0.119 is confirmed working.
- **Build Environment**: Standard Linux environment with dotnet CLI.
- **No Database Required**: Uses in-memory storage for simplicity.

### Essential Commands - VALIDATED AND WORKING

#### Building the Application
```bash
cd /path/to/app-todolist
dotnet build
```
- **Build Time**: ~13 seconds. NEVER CANCEL - set timeout to 60+ seconds minimum.
- **Build Output**: TodoListApi.dll in `TodoListApi/bin/Debug/net8.0/`
- **Build Success**: Produces zero warnings and zero errors when successful.

#### Running the Application
```bash
cd TodoListApi
dotnet run
```
- **Startup Time**: ~5-10 seconds. NEVER CANCEL - set timeout to 30+ seconds minimum.
- **Default URL**: http://localhost:5274
- **Startup Indicators**: Look for "Now listening on: http://localhost:5274" message.
- **Environment**: Runs in Development mode by default with Swagger enabled.

#### Accessing the Application
- **Web Interface**: http://localhost:5274 (redirects to /index.html)
- **API Base**: http://localhost:5274/todos
- **Swagger Documentation**: http://localhost:5274/swagger
- **OpenAPI Spec**: http://localhost:5274/swagger/v1/swagger.json

## Validation Scenarios

### CRITICAL: Manual Validation Requirements
After making changes, ALWAYS validate these complete user scenarios:

#### Scenario 1: API Functionality Test
```bash
# 1. Start the application
cd TodoListApi && dotnet run

# 2. Test API endpoints (run these in a separate terminal)
curl -s http://localhost:5274/todos | head -50
curl -s -X POST "http://localhost:5274/todos" -H "Content-Type: application/json" -d '{"title": "Test task", "description": "Testing functionality", "priority": 2}'
curl -s -X POST "http://localhost:5274/todos/1/complete"
curl -s http://localhost:5274/todos?completed=true
```

#### Scenario 2: Web Interface Validation
1. Navigate to http://localhost:5274
2. Verify 6 sample tasks are displayed
3. Create a new task using the form:
   - Enter title and description
   - Select priority level
   - Click "Adicionar Tarefa"
4. Verify task appears in the list immediately
5. Test completing a task by clicking "✅ Concluir"
6. Test filtering: "📋 Todas", "⏳ Pendentes", "✅ Concluídas"
7. Verify priority badges display correctly (🟢 Baixa, 🟡 Média, 🔴 Alta)

#### Scenario 3: Build and Run Integration Test
```bash
# Full build and run sequence
cd /path/to/app-todolist
dotnet build
cd TodoListApi
dotnet run &
sleep 10
curl -s http://localhost:5274/todos | grep -q "Estudar ASP.NET Core"
echo "Integration test passed"
```

## Project Structure and Key Files

### Root Directory
```
app-todolist/
├── .gitignore              # .NET specific gitignore
├── README.md               # Portuguese documentation
├── EXEMPLOS.md             # API usage examples
├── INTERFACE-WEB.md        # Web interface documentation
├── LICENSE                 # MIT license
├── app-todolist.sln        # Visual Studio solution file
└── TodoListApi/            # Main project directory
```

### TodoListApi Directory
```
TodoListApi/
├── Program.cs              # Main entry point and API endpoints
├── TodoListApi.csproj      # Project file (.NET 8)
├── TodoListApi.http        # HTTP request examples
├── appsettings.json        # Configuration
├── appsettings.Development.json
├── Models/
│   └── ToDoItem.cs         # Domain model
├── DTOs/
│   └── ToDoItemDto.cs      # Data Transfer Objects
├── Services/
│   └── ToDoService.cs      # Business logic and in-memory storage
└── wwwroot/
    └── index.html          # Web interface (HTML/CSS/JS)
```

## Key Technical Details

### Technologies Used
- **.NET 8**: Target framework
- **ASP.NET Core**: Web framework with minimal APIs
- **Swagger/OpenAPI**: API documentation (Swashbuckle.AspNetCore 6.6.2)
- **In-Memory Storage**: No database required
- **CORS**: Configured for development (allows any origin)

### API Endpoints
- `GET /todos` - List all tasks (supports ?completed=true/false filter)
- `GET /todos/{id}` - Get specific task
- `POST /todos` - Create new task
- `PUT /todos/{id}` - Update existing task
- `DELETE /todos/{id}` - Delete task
- `POST /todos/{id}/complete` - Mark task as completed
- `POST /todos/{id}/uncomplete` - Mark task as pending

### Sample Data
The application starts with 6 pre-loaded sample tasks covering different priorities and completion states. This enables immediate testing without data setup.

## Development Workflow

### Making Changes
1. **Always build first**: `dotnet build` to verify current state
2. **Make minimal changes**: Focus on specific requirements only
3. **Test immediately**: Run validation scenarios after each change
4. **Verify web interface**: Always test the HTML interface, not just the API

### Common Tasks

#### Adding New API Endpoints
1. Modify `Program.cs` to add new endpoint mapping
2. Update `IToDoService` interface if business logic changes needed
3. Implement in `ToDoService.cs`
4. Test with curl commands
5. Update `TodoListApi.http` with examples

#### Modifying the Data Model
1. Update `Models/ToDoItem.cs`
2. Update `DTOs/ToDoItemDto.cs` if needed
3. Modify `ToDoService.cs` for any business logic changes
4. Test API endpoints to ensure serialization works
5. Test web interface to ensure display works

#### Updating the Web Interface
1. Modify `wwwroot/index.html`
2. Test in browser at http://localhost:5274
3. Verify responsive design works
4. Test all interactive features (create, edit, delete, complete)

## Important Notes

### Data Persistence
- **In-Memory Only**: All data is lost when application restarts
- **Sample Data**: 6 tasks are automatically created on startup
- **No Database**: No Entity Framework or database configuration needed

### No Test Infrastructure
- **No Unit Tests**: No existing test projects or test framework
- **Manual Testing Required**: Always use the validation scenarios above
- **No CI/CD**: No GitHub Actions or automated testing

### Troubleshooting

#### Port Already in Use
If port 5274 is unavailable, dotnet will automatically assign a different port. Check the console output for the actual URL.

#### Build Failures
- Ensure .NET 8 SDK is installed: `dotnet --version`
- Clean and rebuild: `dotnet clean && dotnet build`
- Check for syntax errors in C# files

#### Application Won't Start
- Check for compilation errors: `dotnet build`
- Verify appsettings.json is valid JSON
- Ensure no other instance is running on the same port

#### API Returns 404
- Verify application is running: check console output
- Ensure using correct URL: http://localhost:5274/todos
- Check that CORS is properly configured in Program.cs

### Performance Expectations
- **Build Time**: 10-15 seconds on standard hardware
- **Startup Time**: 5-10 seconds
- **API Response**: Sub-second for all endpoints
- **Memory Usage**: Minimal (~50MB) due to in-memory storage

## File Reference

### Core Configuration Files
```bash
# View project configuration
cat TodoListApi/TodoListApi.csproj
cat TodoListApi/appsettings.json
cat TodoListApi/appsettings.Development.json
```

### Key Source Files
```bash
# Main application entry point
cat TodoListApi/Program.cs

# Domain model
cat TodoListApi/Models/ToDoItem.cs

# Business logic
cat TodoListApi/Services/ToDoService.cs

# DTOs
cat TodoListApi/DTOs/ToDoItemDto.cs
```

### Documentation Files
```bash
# Portuguese README with full documentation
cat README.md

# API usage examples
cat EXEMPLOS.md

# Web interface documentation
cat INTERFACE-WEB.md
```

### HTTP Examples
```bash
# Ready-to-use HTTP requests for testing
cat TodoListApi/TodoListApi.http
```

Remember: This is a development-focused application with in-memory storage. Perfect for learning, prototyping, and demonstrating ASP.NET Core minimal APIs concepts.