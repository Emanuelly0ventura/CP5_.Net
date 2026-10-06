# Catálogo de Jogos - .NET & MongoDB

Projeto atualizado a partir da prova anterior para atender à avaliação prática de .NET & MongoDB (CRUD + Desafio de Agregação).

## Integrantes
### Carolina Nascimento Gonçalves
RM564786 - 2TDSPJ

### Julia Sayuri Kina
RM564555 - 2TDSPJ

### Emanuelly Ventura do Nascimento
RM562339 - 2TDSPJ

## Tecnologias

- .NET 9
- ASP.NET Core Web API
- MongoDB
- MongoDB.Driver
- Swagger

## Arquitetura

A aplicação utiliza a separação:

**Controller → JogoService → IJogoRepository → MongoDB**

A conexão e o nome do banco são configurados no `appsettings.json` usando `IOptions<MongoDbSettings>`.

## Como executar

### 1. Subir o MongoDB com Docker

```powershell
docker run -d -p 27017:27017 --name mongo-local mongo
```

Se o container já existir:

```powershell
docker start mongo-local
```

### 2. Restaurar e executar

Na pasta que contém a solução:

```powershell
dotnet restore
dotnet run --project Todo.Api
```

Acesse o Swagger pela URL exibida no terminal, normalmente:

```text
https://localhost:xxxx/swagger
```

## Endpoints

### CRUD

- `POST /api/jogos`
- `GET /api/jogos`
- `GET /api/jogos/{id}`
- `PUT /api/jogos/{id}`
- `DELETE /api/jogos/{id}`

### Filtro

```text
GET /api/jogos/busca?plataforma=PlayStation%203&precoMaximo=200
```

Retorna jogos da plataforma informada com preço menor ou igual ao limite.

### Relatório de estoque

```text
GET /api/jogos/relatorio-estoque
```

Agrupa os jogos por plataforma e calcula:

- quantidade total de títulos;
- valor total do inventário (`Preco × Estoque`).

## Exemplo de cadastro

```json
{
  "titulo": "Gran Turismo 6",
  "plataforma": "PlayStation 3",
  "genero": "Corrida",
  "preco": 149.90,
  "anoLancamento": 2013,
  "estoque": 10
}
```
