
# ContactManager - Projeto de Gerenciamento de Contatos

##  Visão Geral

**ContactManager** é uma aplicação completa para gerenciamento de contatos. O sistema permite realizar operações CRUD (Criar, Ler, Atualizar e Deletar) em uma base de contatos, com validações.

---

##  Arquitetura do Sistema

### Backend (ASP.NET)

Desenvolvido seguindo princípios de arquitetura limpa, com separação clara de responsabilidades em camadas:

#### Estrutura de Camadas

- **API**
  - Responsável pela apresentação do backend
  - Contém os controllers que recebem e respondem às requisições HTTP
  - Gerencia rotas e endpoints da aplicação

- **Application**
  - Contém os DTOs (Data Transfer Objects) para entrada e saída de dados
    - **Input DTOs**: Validam e formatam dados recebidos do frontend
    - **Output DTOs**: Estruturam dados para retorno ao cliente
  - **Services**: Implementam a lógica de negócio e coordenam a comunicação com o repositório

- **Domain**
  - Define as entidades do sistema (Contato)
  - Contém interfaces de repositório que definem operações de persistência
  - Estabelece regras de negócio independentes de infraestrutura

- **Infrastructure**
  - Implementa as interfaces de repositório definidas na camada de domínio
  - Gerencia a conexão com o banco de dados
  - Contém migrações e configurações de persistência

###  Funcionalidades Implementadas

- CRUD completo de contatos
- Validação de dados nos DTOs (campos obrigatórios, máscaras para telefone)
- Tratamento de erros e respostas HTTP apropriadas

---

### Frontend (WPF Desktop)

Desenvolvido com **WPF (Windows Presentation Foundation)**.

#### Estrutura

- **Views**: Interfaces de usuário para listar, criar, editar e excluir contatos
- **Services**: Gerencia a comunicação com as APIs do backend
- **Models**: Representação dos dados na aplicação cliente
- **Validations**: Implementação de validações de campos no lado do cliente

#### Funcionalidades

- Listagem de todos os contatos cadastrados
- Formulário para criação de novos contatos
- Edição de contatos existentes
- Exclusão de contatos
- Validação de campos (ex: formato de telefone)

---

##  Implantação

### Azure Cloud

- **API**: Implantada como um Azure App Service
- **Banco de Dados**: Azure SQL Database para armazenamento persistente

### CI/CD com GitHub Actions

- Commits na branch `master` disparam automaticamente o processo de deploy

---


##  Como Executar o Projeto

### Requisitos

- .NET 8.0 ou superior  
- Visual Studio 2022 (recomendado)  
- SQL Server (local) para desenvolvimento  

### Passos para Execução Local

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/frankiwillian/ContactManager
   ```

2. **Abra a solução no Visual Studio:**

   ```bash
   cd ContactManager
   start ContactManager.sln
   ```

3. **String de conexão já configurada no appsettings.json apontando para o meu banco de dados da Azure** Caso queira testar em um banco de dados locar, foi criada Migration inicial.


4. **Execute o projeto** (pressione **F5** no Visual Studio).

---

## Tecnologias Utilizadas

### Backend

- ASP.NET Core 8.0  
- Entity Framework Core  

### Frontend

- WPF (.NET 8.0)  
- MVVM Pattern  

### DevOps

- Azure App Service  
- Azure SQL Database  
- GitHub Actions (CI/CD)
