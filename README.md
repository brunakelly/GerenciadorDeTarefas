# Gerenciador de Tarefas

API REST para gerenciamento de tarefas, desenvolvida em C# com ASP.NET Core. Inclui documentação interativa via Swagger.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 10 ou compatível com o projeto)

## Como rodar o projeto

1. **Clone o repositório** (se ainda não tiver o código local):
   ```bash
   git clone https://github.com/brunakelly/GerenciadorDeTarefas.git
   cd GerenciadorDeTarefas
   ```

2. **Restaure as dependências**:
   ```bash
   dotnet restore
   ```

3. **Execute a API**:
   ```bash
   dotnet run --project GerenciadorDeTarefas
   ```
   Ou, pelo Visual Studio / Rider: abra a solução e execute o projeto **GerenciadorDeTarefas** (API).

4. A aplicação sobe em:
   - **HTTP:** `http://localhost:5042`
   - **HTTPS:** `https://localhost:7013`

## Como visualizar e testar (Swagger)

Com a API rodando, abra no navegador:

- **Swagger (documentação e testes):**  
  [http://localhost:5042/swagger](http://localhost:5042/swagger)  
  ou [https://localhost:7013/swagger](https://localhost:7013/swagger)

No Swagger você pode:
- Ver todos os endpoints e suas descrições
- Testar as requisições (criar, listar, atualizar e excluir tarefas) direto na interface

## Endpoints principais

| Método | Rota            | Descrição           |
|--------|-----------------|---------------------|
| POST   | `/api/tasks`    | Criar tarefa        |
| GET    | `/api/tasks`    | Listar todas        |
| GET    | `/api/tasks/{id}` | Buscar por ID     |
| PUT    | `/api/tasks/{id}` | Atualizar tarefa  |
| DELETE | `/api/tasks/{id}` | Excluir tarefa    |

## Estrutura do projeto

- **GerenciadorDeTarefas** – API (Controllers, Program.cs)
- **GerenciadorDeTarefas.Application** – Serviços e regras de negócio
- **GerenciadorDeTarefas.Communication** – DTOs, enums, requests e responses

## Observação

Os dados são armazenados **em memória**. Ao reiniciar a API, as tarefas são perdidas. Para persistência entre reinicializações, seria necessário integrar um banco de dados.
