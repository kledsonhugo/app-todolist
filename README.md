# 📝 Todo List API

Uma API REST simples e eficiente para gerenciar listas de tarefas (to-do list) desenvolvida com ASP.NET Core e APIs mínimas.

## 🚀 Funcionalidades

- ✅ Criar novas tarefas
- 📖 Listar todas as tarefas
- 🔍 Filtrar tarefas por status (concluída/pendente)
- ✏️ Atualizar tarefas existentes
- ✅ Marcar tarefas como concluídas/pendentes
- 🗑️ Deletar tarefas
- 🎯 Sistema de prioridades (1=Baixa, 2=Média, 3=Alta)

## 🛠️ Tecnologias

- .NET 8
- ASP.NET Core (APIs mínimas)
- Swagger/OpenAPI para documentação
- Armazenamento em memória (para simplicidade)

## 📦 Estrutura do Projeto

```
TodoListApi/
├── Models/
│   └── ToDoItem.cs          # Modelo da tarefa
├── DTOs/
│   └── ToDoItemDto.cs       # Data Transfer Objects
├── Services/
│   └── ToDoService.cs       # Lógica de negócio
├── Program.cs               # Configuração e endpoints da API
└── TodoListApi.http         # Exemplos de requisições
```

## 🏃‍♂️ Como Executar

1. **Navegue até o diretório do projeto:**
   ```bash
   cd TodoListApi
   ```

2. **Execute a aplicação:**
   ```bash
   dotnet run
   ```

3. **Acesse a aplicação:**
   - **Interface Web**: http://localhost:5274 (página principal)
   - **Documentação Swagger**: http://localhost:5274/swagger
   - **API REST**: http://localhost:5274/todos

## 🌐 Interface Web

A aplicação inclui uma interface web moderna e responsiva que permite:

- ✅ **Adicionar** novas tarefas com título, descrição e prioridade
- 📋 **Listar** todas as tarefas ou filtrar por status
- ✏️ **Editar** tarefas existentes
- ✅ **Marcar** tarefas como concluídas/pendentes
- 🗑️ **Excluir** tarefas
- 📱 **Design responsivo** para desktop e mobile

### 🎯 Funcionalidades da Interface:
- Interface intuitiva e moderna
- Filtros rápidos (Todas, Pendentes, Concluídas)
- Sistema visual de prioridades
- Mensagens de feedback em tempo real
- Formulário dinâmico para criação/edição
- Data e hora de criação/conclusão
- Confirmação antes de excluir tarefas

A aplicação já vem com **6 tarefas de exemplo** para facilitar o teste!

## 📚 Endpoints da API

### 📋 Listar Tarefas
```http
GET /todos
GET /todos?completed=true   # Apenas concluídas
GET /todos?completed=false  # Apenas pendentes
```

### 👀 Obter Tarefa por ID
```http
GET /todos/{id}
```

### ➕ Criar Nova Tarefa
```http
POST /todos
Content-Type: application/json

{
  "title": "Minha tarefa",
  "description": "Descrição opcional",
  "priority": 1
}
```

### ✏️ Atualizar Tarefa
```http
PUT /todos/{id}
Content-Type: application/json

{
  "title": "Título atualizado",
  "description": "Nova descrição",
  "isCompleted": true,
  "priority": 2
}
```

### ✅ Marcar como Concluída
```http
POST /todos/{id}/complete
```

### ❌ Marcar como Pendente
```http
POST /todos/{id}/uncomplete
```

### 🗑️ Deletar Tarefa
```http
DELETE /todos/{id}
```

## 📄 Modelo de Dados

### ToDoItem
```json
{
  "id": 1,
  "title": "Estudar .NET",
  "description": "Aprender sobre APIs mínimas",
  "isCompleted": false,
  "createdAt": "2025-09-04T10:00:00Z",
  "completedAt": null,
  "priority": 2
}
```

### Campos:
- **id**: Identificador único (gerado automaticamente)
- **title**: Título da tarefa (obrigatório)
- **description**: Descrição opcional
- **isCompleted**: Status de conclusão
- **createdAt**: Data/hora de criação
- **completedAt**: Data/hora de conclusão (quando aplicável)
- **priority**: Prioridade (1=Baixa, 2=Média, 3=Alta)

## 🧪 Testando a API

Use o arquivo `TodoListApi.http` incluído no projeto para testar todos os endpoints. No VS Code com a extensão REST Client, você pode executar as requisições diretamente do arquivo.

## 🔄 Próximos Passos

Para uma versão mais robusta, considere:

- 🗄️ Integração com banco de dados (Entity Framework Core)
- 🔐 Autenticação e autorização
- 📱 Interface web (Blazor, React, etc.)
- 🏷️ Sistema de tags/categorias
- 📅 Datas de vencimento
- 🔔 Notificações
- 📊 Relatórios e estatísticas

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.