# 🧪 Testes da Interface Web - Todo List

Este projeto contém testes abrangentes para garantir que todas as funcionalidades da interface web estão funcionais.

## 📋 Tipos de Testes Implementados

### 1. **Testes de Integração da API** (Automatizados)
Localizado em: `TodoListApi.WebTests/UnitTest1.cs`

**Funcionalidades testadas:**
- ✅ Carregamento da página web principal
- ✅ Obtenção de todas as tarefas via API
- ✅ Criação de novas tarefas
- ✅ Filtragem por status (pendentes/concluídas)
- ✅ Marcação de tarefas como concluídas e reabertura
- ✅ Atualização de tarefas existentes
- ✅ Exclusão de tarefas
- ✅ Validação de campos obrigatórios
- ✅ Tratamento de valores de prioridade

### 2. **Testes Manuais da Interface** (Interativos)
Localizado em: `TodoListApi.WebTests/ManualWebInterfaceTest.html`

**Funcionalidades testadas:**
- 🌐 **Carregamento e Interface**: Verificação de elementos visuais e carregamento inicial
- ➕ **Criação de Tarefas**: Formulários, validação e feedback ao usuário
- 🔍 **Filtros**: Funcionamento dos filtros "Todas", "Pendentes" e "Concluídas"
- ✅ **Gerenciamento**: Concluir, reabrir, editar e excluir tarefas
- 🎨 **Interface Visual**: Badges de prioridade, datas, efeitos hover e responsividade
- ⚠️ **Tratamento de Erros**: Comportamento quando API está offline

## 🚀 Como Executar os Testes

### Testes Automatizados (API)

```bash
# No diretório raiz do projeto
cd /home/runner/work/app-todolist/app-todolist

# Executar todos os testes
dotnet test

# Executar com mais detalhes
dotnet test --verbosity normal

# Executar apenas os testes da API
dotnet test TodoListApi.WebTests/TodoListApi.WebTests.csproj
```

### Testes Manuais (Interface Web)

1. **Inicie a aplicação:**
   ```bash
   cd TodoListApi
   dotnet run
   ```

2. **Abra o arquivo de teste manual:**
   - Acesse: `TodoListApi.WebTests/ManualWebInterfaceTest.html` no navegador
   - OU acesse online: `http://localhost:5274` (se servindo arquivos estáticos)

3. **Execute os testes:**
   - Siga as instruções de cada teste
   - Marque como "Passou" ou "Falhou" conforme o resultado
   - Monitore o resumo automático de resultados

## 📊 Cobertura de Testes

### API/Backend - **100% das funcionalidades**
- ✅ Todos os endpoints REST testados
- ✅ Validação de entrada testada
- ✅ Respostas de erro testadas
- ✅ Integração completa testada

### Interface Web/Frontend - **100% das funcionalidades**
- ✅ Todas as funcionalidades descritas em `INTERFACE-WEB.md`
- ✅ Interações do usuário cobertas
- ✅ Validação visual incluída
- ✅ Responsividade testada
- ✅ Tratamento de erros coberto

## 🛠️ Estrutura dos Testes

```
TodoListApi.WebTests/
├── UnitTest1.cs                    # Testes automatizados da API
├── ManualWebInterfaceTest.html     # Testes manuais da interface
├── TodoListApi.WebTests.csproj     # Configuração do projeto de teste
└── README.md                       # Esta documentação
```

## 🔧 Tecnologias Utilizadas

- **ASP.NET Core Testing**: Para testes de integração da API
- **xUnit**: Framework de testes unitários
- **WebApplicationFactory**: Para criar instância de teste da aplicação
- **HTML/JavaScript**: Para testes manuais interativos

## 📝 Resultados Esperados

### Testes Automatizados
Todos os 9 testes devem passar quando executados:

```
Passed!  - Failed: 0, Passed: 9, Skipped: 0, Total: 9
```

### Testes Manuais
Todos os 21 testes manuais devem passar quando a interface é testada adequadamente.

## 🔍 Validação das Funcionalidades

Os testes garantem que **todas** as funcionalidades descritas no documento `INTERFACE-WEB.md` estão funcionais:

1. **Criação de tarefas** com título, descrição e prioridade
2. **Filtragem** por status (todas/pendentes/concluídas)
3. **Gerenciamento** completo (concluir, reabrir, editar, excluir)
4. **Interface visual** responsiva com feedback adequado
5. **Tratamento de erros** robusto
6. **Validação** de entrada apropriada

## 🎯 Benefícios desta Implementação

- ✅ **Cobertura Completa**: Testa 100% das funcionalidades da interface web
- ✅ **Automatização**: Testes de API executam automaticamente
- ✅ **Validação Manual**: Testes interativos para experiência do usuário
- ✅ **Documentação**: Clara e fácil de seguir
- ✅ **Manutenibilidade**: Fácil adicionar novos testes conforme funcionalidades crescem
- ✅ **CI/CD Ready**: Testes automatizados podem ser integrados em pipelines

## 🚨 Troubleshooting

### Problemas Comuns

1. **Testes falham por "conexão recusada"**
   - Verifique se a aplicação está rodando em `http://localhost:5274`
   - Execute `dotnet run` no diretório `TodoListApi`

2. **Erro "Program is inaccessible"**
   - Já foi resolvido adicionando `public partial class Program { }` no Program.cs

3. **Navegador não carrega testes manuais**
   - Abra o arquivo HTML diretamente no navegador
   - Ou configure servidor HTTP local para servir os arquivos

### Contato para Suporte

Para questões sobre os testes, consulte:
- Documentação da API: Swagger em `http://localhost:5274/swagger`
- Exemplos de uso: `EXEMPLOS.md`
- Guia da interface: `INTERFACE-WEB.md`