# API Rodoviária 🚌

API REST para um sistema de venda de passagens de ônibus, feita em **C# / ASP.NET Core** com **Entity Framework Core** e **PostgreSQL**, seguindo uma arquitetura em camadas (Controllers → Casos de Uso → Repositórios → Banco de Dados).

## Funcionalidades

- **Cadastro de ônibus** — inclui motorista responsável, endereço de início e de fim da rota
- **Cadastro de motorista** — um motorista nunca fica em duas viagens no mesmo período
- **Cadastro de usuário (cliente)** — quem compra as cadeiras
- **Escolha de cadeiras** — o cliente pode reservar uma ou várias cadeiras de uma viagem
- **Autenticação** — login com **JWT** entregue via **cookie HttpOnly**

## Tecnologias

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- JWT Bearer Authentication
- BCrypt.Net (hash de senha)
- Swagger / OpenAPI

## Arquitetura

```
Controller ──► Caso de Uso ──► Repositório ──► DbContext ──► PostgreSQL
 (HTTP)         (regras)       (acesso a dados)  (EF Core)
```

| Camada | Responsabilidade |
|---|---|
| `Controllers` | Recebe as requisições HTTP e devolve as respostas |
| `Application/UseCase` | Regras de negócio de cada ação do sistema |
| `Domain/Models` | Entidades que representam as tabelas do banco |
| `Domain/Interfaces` | Contratos dos repositórios |
| `Infrastructure/DataAccess` | `DbContext`, mapeamentos e repositórios |
| `Infrastructure/Services` | Hash de senha e geração de token JWT |
| `Middleware` | Tratamento centralizado de erros |

## Modelo de dados

O banco é composto pelas tabelas: `Perfil`, `Usuario`, `Motorista`, `Onibus`, `Cadeira`, `Rota`, `Viagem`, `Reserva` e `ReservaCadeira` (tabela de ligação entre reserva e cadeiras escolhidas).

- Um **Usuário** tem um **Perfil** (`Admin` ou `Cliente`)
- Uma **Viagem** liga um **Ônibus**, um **Motorista** e uma **Rota**
- Uma **Reserva** pertence a um **Usuário** e a uma **Viagem**, e pode conter várias **Cadeiras**

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- `dotnet-ef` (ferramenta de migrations):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/<seu-usuario>/API-Rodoviaria.git
   cd API-Rodoviaria
   ```

2. Configure a conexão com o banco e o segredo do JWT em `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "Padrao": "Host=localhost;Port=5432;Database=rodoviaria;Username=postgres;Password=SUA_SENHA_AQUI"
     },
     "Jwt": {
       "Key": "troque-esta-chave-por-uma-bem-grande-com-mais-de-32-caracteres",
       "Issuer": "API-Rodoviaria",
       "Audience": "API-Rodoviaria-Clientes",
       "ExpireMinutes": 60
     }
   }
   ```
   > Em produção, não deixe a chave do JWT no arquivo — use *User Secrets* ou uma variável de ambiente.

3. Aplique as migrations:
   ```bash
   dotnet ef migrations add Inicial
   dotnet ef database update
   ```

4. Rode o projeto:
   ```bash
   dotnet run
   ```

Um usuário **Admin** é criado automaticamente na primeira execução (dados em `AdminInicial`, no `appsettings.json`).

## Uso

Com o projeto rodando, abra o Swagger (`/swagger`) e siga esta ordem:

1. `POST /api/auth/login` — login como Admin
2. `POST /api/motoristas` — cadastrar motorista
3. `POST /api/onibus` — cadastrar ônibus (com motorista e rota)
4. `POST /api/usuarios` — cadastrar um cliente (rota pública)
5. `POST /api/auth/login` — login como cliente
6. `GET /api/viagens` — listar viagens disponíveis
7. `GET /api/viagens/{id}/cadeiras` — ver cadeiras livres da viagem
8. `POST /api/reservas` — reservar uma ou mais cadeiras

O login grava o token JWT em um cookie `HttpOnly`; as próximas requisições autenticadas são feitas automaticamente pelo navegador.

## Segurança

- Senhas nunca são salvas em texto puro — são armazenadas com hash (**BCrypt**)
- O token JWT fica em um cookie `HttpOnly`, `Secure` e `SameSite=Strict`
- Rotas administrativas exigem `[Authorize(Roles = "Admin")]`
- A escolha simultânea da mesma cadeira por dois clientes é resolvida com transação `Serializable` no banco
- 
