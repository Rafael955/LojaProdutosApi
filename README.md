# ProdutosApp

ProdutosApp é uma aplicação web desenvolvida em Blazor WebAssembly com backend em .NET 9, focada no gerenciamento de produtos e fornecedores. O sistema oferece funcionalidades de cadastro, consulta e autenticação de usuários, além de integração com mensageria e logging.

> **Atenção:** A implementação da interface em Blazor WebAssembly ainda está em desenvolvimento. Algumas funcionalidades podem não estar completas ou podem sofrer alterações nas próximas versões.

## Funcionalidades

- **Cadastro e consulta de produtos**
- **Cadastro e consulta de fornecedores**
- **Dashboard de fornecedores**
- **Autenticação e criação de usuários**
- **Integração com RabbitMQ para mensageria**
- **Registro de logs customizados**
- **Interface moderna e responsiva com Blazor WebAssembly** (em desenvolvimento)

## Estrutura do Projeto

- `ProdutosApp.UI`: Frontend Blazor WebAssembly
- `ProdutosApp.Api`: Backend ASP.NET Core (.NET 9)
- `ProdutosApp.Domain`: Entidades e interfaces de domínio
- `ProdutosApp.Infra.Data`: Persistência de dados (Entity Framework)
- `ProdutosApp.Infra.Logging`: Implementação de logging customizado
- `ProdutosApp.Infra.Message`: Integração com RabbitMQ para mensageria

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (para RabbitMQ via docker-compose)
- [Node.js](https://nodejs.org/) (opcional, para desenvolvimento frontend)

## Como executar

1. Clone o repositório: git clone <url-do-repositorio> cd ProdutosApp
2. Suba os serviços necessários: docker-compose up -d
3. Execute o backend: cd ProdutosApp.Api dotnet run
4. Execute o frontend: cd ProdutosApp.UI dotnet run
5. Acesse a aplicação em [http://localhost:5000](http://localhost:5000) (ou porta configurada).

## Configuração

- As configurações de conexão (banco de dados, RabbitMQ, MongoDB, MailHog) estão no arquivo `docker-compose.yml`.
- O frontend se comunica com a API via HttpClient configurado em `ProdutosApp.UI/Configurations/HttpClientName.cs`.

## Principais Telas

- Cadastro e consulta de produtos e fornecedores
- Dashboard de fornecedores
- Autenticação e criação de usuários

## Licença

Este projeto está licenciado sob os termos da licença MIT.

---

> Desenvolvido com Blazor WebAssembly e .NET 9.