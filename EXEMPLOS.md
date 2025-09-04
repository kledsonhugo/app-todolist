# 🧪 Exemplos de Uso da Todo List API

Este arquivo contém exemplos práticos de como usar a API de lista de tarefas.

## 🚀 Pré-requisitos

1. Certifique-se de que a aplicação está rodando:
   ```bash
   cd TodoListApi
   dotnet run
   ```

2. A aplicação estará disponível em: http://localhost:5274
3. Documentação Swagger: http://localhost:5274/swagger

## 📝 Cenários de Uso

### Cenário 1: Criando suas primeiras tarefas

```bash
# 1. Criar tarefa de trabalho (alta prioridade)
curl -X POST "http://localhost:5274/todos" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Revisar código do projeto",
    "description": "Fazer code review das alterações da sprint",
    "priority": 3
  }'

# 2. Criar tarefa pessoal (prioridade média)
curl -X POST "http://localhost:5274/todos" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Fazer compras no supermercado",
    "description": "Comprar ingredientes para o jantar de domingo",
    "priority": 2
  }'

# 3. Criar tarefa simples (baixa prioridade)
curl -X POST "http://localhost:5274/todos" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Ler artigo sobre .NET",
    "description": "Artigo sobre APIs mínimas no ASP.NET Core",
    "priority": 1
  }'
```

### Cenário 2: Consultando suas tarefas

```bash
# Listar todas as tarefas
curl -X GET "http://localhost:5274/todos"

# Listar apenas tarefas pendentes
curl -X GET "http://localhost:5274/todos?completed=false"

# Obter detalhes de uma tarefa específica
curl -X GET "http://localhost:5274/todos/1"
```

### Cenário 3: Gerenciando o progresso

```bash
# Marcar a primeira tarefa como concluída
curl -X POST "http://localhost:5274/todos/1/complete"

# Atualizar uma tarefa (mudar prioridade e descrição)
curl -X PUT "http://localhost:5274/todos/2" \
  -H "Content-Type: application/json" \
  -d '{
    "description": "Comprar ingredientes para o jantar de domingo - URGENTE!",
    "priority": 3
  }'

# Marcar tarefa como não concluída (se necessário)
curl -X POST "http://localhost:5274/todos/1/uncomplete"
```

### Cenário 4: Limpeza e organização

```bash
# Ver apenas tarefas concluídas
curl -X GET "http://localhost:5274/todos?completed=true"

# Deletar uma tarefa que não é mais necessária
curl -X DELETE "http://localhost:5274/todos/3"

# Verificar lista final
curl -X GET "http://localhost:5274/todos"
```

## 🎯 Dicas de Uso

### Prioridades
- **1 (Baixa)**: Tarefas que podem ser feitas quando houver tempo
- **2 (Média)**: Tarefas importantes mas não urgentes
- **3 (Alta)**: Tarefas urgentes que devem ser feitas primeiro

### Filtros Úteis
- `?completed=false` - Ver apenas tarefas pendentes
- `?completed=true` - Ver apenas tarefas concluídas

### Campos Opcionais na Atualização
Você pode atualizar apenas os campos que precisar:
```json
{
  "title": "Novo título"           // Só o título
}

{
  "isCompleted": true             // Só o status
}

{
  "priority": 3,                  // Prioridade e descrição
  "description": "Nova descrição"
}
```

## 🔧 Troubleshooting

### Porta em uso
Se a porta 5274 estiver em uso, a aplicação usará uma porta diferente. Verifique a saída do `dotnet run` para ver qual porta está sendo usada.

### CORS (se usando frontend)
A API está configurada apenas para desenvolvimento local. Para usar com frontend em domínio diferente, será necessário configurar CORS.

### Dados em memória
Lembre-se que os dados são armazenados em memória. Ao reiniciar a aplicação, todas as tarefas serão perdidas.

## 📱 Testando com a Interface Swagger

1. Acesse: http://localhost:5274/swagger
2. Expanda os endpoints para ver todos os parâmetros
3. Use "Try it out" para testar diretamente no navegador
4. A interface Swagger oferece uma forma visual e interativa de testar a API
