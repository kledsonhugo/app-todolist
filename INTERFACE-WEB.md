# 🌐 Guia da Interface Web - Todo List

Bem-vindo à interface web da aplicação Todo List! Este guia vai te ajudar a aproveitar ao máximo todas as funcionalidades.

## 🚀 Acessando a Interface

1. Execute a aplicação: `dotnet run` (no diretório TodoListApi)
2. Abra seu navegador e acesse: **http://localhost:5274**
3. A página principal será carregada automaticamente

## 📋 Funcionalidades Principais

### ➕ Criando uma Nova Tarefa

1. **Título**: Campo obrigatório - descreva sua tarefa de forma clara
2. **Descrição**: Opcional - adicione detalhes adicionais
3. **Prioridade**: 
   - 🟢 **Baixa** (1) - Tarefas que podem esperar
   - 🟡 **Média** (2) - Tarefas importantes (padrão)
   - 🔴 **Alta** (3) - Tarefas urgentes
4. Clique em **"Adicionar Tarefa"**

### 🔍 Filtrando Tarefas

Use os botões de filtro para visualizar:
- **📋 Todas**: Mostra todas as tarefas
- **⏳ Pendentes**: Apenas tarefas não concluídas
- **✅ Concluídas**: Apenas tarefas finalizadas

### ✏️ Gerenciando Tarefas

Cada tarefa possui botões de ação:

- **✅ Concluir**: Marca a tarefa como finalizada
- **↩️ Reabrir**: Desmarca tarefa concluída (volta para pendente)
- **✏️ Editar**: Permite alterar título, descrição e prioridade
- **🗑️ Excluir**: Remove a tarefa permanentemente (com confirmação)

### 📊 Informações Exibidas

Para cada tarefa você pode ver:
- **Título** e **descrição**
- **Badge de prioridade** colorido
- **Data/hora de criação**
- **Data/hora de conclusão** (quando aplicável)
- **Status visual** (tarefas concluídas ficam com aparência diferenciada)

## 🎨 Design e Usabilidade

### 🎯 Indicadores Visuais
- **Cores das prioridades**:
  - Verde: Baixa prioridade
  - Amarelo: Média prioridade  
  - Vermelho: Alta prioridade
- **Estado das tarefas**:
  - Tarefas concluídas ficam com texto riscado e opacidade reduzida
  - Hover effects para melhor interação

### 📱 Responsividade
- **Desktop**: Layout em duas colunas otimizado
- **Mobile**: Interface adaptada para telas pequenas
- **Tablet**: Experiência intermediária balanceada

### ⚡ Feedback em Tempo Real
- **Mensagens de sucesso** em verde para ações bem-sucedidas
- **Mensagens de erro** em vermelho para problemas
- **Loading states** durante operações da API
- **Auto-hide** das mensagens após 3 segundos

## 🧪 Tarefas de Exemplo

A aplicação inicia com 6 tarefas de exemplo:

1. **Estudar ASP.NET Core** (Alta prioridade, Pendente)
2. **Fazer compras no supermercado** (Média prioridade, Pendente)
3. **Exercitar-se** (Média prioridade, **Concluída**)
4. **Ler documentação do .NET** (Baixa prioridade, Pendente)
5. **Reunião de equipe** (Alta prioridade, **Concluída**)
6. **Organizar área de trabalho** (Baixa prioridade, Pendente)

## 🔧 Dicas de Uso

### ✅ Boas Práticas
- Use **títulos descritivos** mas concisos
- Aproveite o campo **descrição** para detalhes importantes
- Defina **prioridades** realistas para melhor organização
- **Filtre as tarefas** para focar no que importa no momento

### ⚠️ Limitações Atuais
- **Dados em memória**: As tarefas são perdidas ao reiniciar a aplicação
- **Edição simplificada**: Usa prompts do navegador (pode ser melhorado com modais)
- **Sem autenticação**: Todos têm acesso às mesmas tarefas

### 🚀 Melhorias Futuras
- Arrastar e soltar para reordenar
- Categorias/tags para organização
- Datas de vencimento
- Notificações
- Sincronização com calendário
- Tema claro/escuro

## 🔄 Integração com a API

A interface web utiliza JavaScript puro (Vanilla JS) para comunicação com a API REST:

- **GET /todos** - Carrega todas as tarefas
- **GET /todos?completed=true/false** - Filtra por status
- **POST /todos** - Cria nova tarefa
- **PUT /todos/{id}** - Atualiza tarefa
- **POST /todos/{id}/complete** - Marca como concluída
- **POST /todos/{id}/uncomplete** - Marca como pendente
- **DELETE /todos/{id}** - Remove tarefa

## 🛠️ Troubleshooting

### ❌ "Erro ao conectar com a API"
- Verifique se a aplicação está rodando: `dotnet run`
- Confirme a URL: http://localhost:5274
- Verifique se não há firewall bloqueando

### ❌ Página não carrega
- Certifique-se de acessar http://localhost:5274 (não https)
- Verifique se a porta 5274 não está ocupada por outra aplicação

### ❌ Funcionalidades não respondem
- Verifique o console do navegador (F12) para erros JavaScript
- Teste os endpoints diretamente no Swagger: http://localhost:5274/swagger

## 📞 Suporte

Para dúvidas ou problemas:
1. Consulte a documentação da API no Swagger
2. Verifique os logs da aplicação no terminal
3. Teste os endpoints individualmente usando curl ou Postman
4. Verifique o arquivo `EXEMPLOS.md` para casos de uso detalhados
