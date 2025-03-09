// This block of code is used to configure the application.
// Add controllers to my project.
// dotnet new apicontroller -n MeuController 

// Add class to my project.
// dotnet new class -n MeuModelo

// If I will want destinate my class or controller to a folder, I will use the -o parameter.
// dotnet new controller -n MeuController -o Controllers


/*Codigos importantes

Essa chamada, serve para criar o projeto (API).
dotnet new webapi -n MinhaApi


Para restaurar as dependências do projeto.
dotnet restore

Para rodar a aplicação.
dotnet run

Caso vá usar o banco de dados com o entity framework rode os comandos abaixo.
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Tools

Lembre-se de configurar no appsettings.json seu banco de dados
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=meubanco;Username=meuusuario;Password=minhasenha"
}


para rodar a migration e criar a database use o comando abaixo.
dotnet ef migrations add Inicial
dotnet ef database update


Caso ao tentar rodar a migration, ele fale que tem erro no build, rode o comando abaixo para buildar.
dotnet build

*/


/*

Caso ao rodar a migration, ocorra o erro abaixo.
PS C:\Projeto Mantrol\Api> dotnet ef migrations add Inicial
Não foi possível executar porque o comando ou arquivo especificado não foi encontrado.
Os possíveis motivos para isso incluem:
  * Você digitou incorretamente um comando dotnet interno.
  * Você pretendia executar um programa .NET, mas dotnet-ef não existe.
  * Você pretendia executar uma ferramenta global, mas não foi possível encontrar um executável com prefixo dotnet com esse nome no PATH.

Use o comando de instação global do EF
dotnet tool install --global dotnet-ef

*/


using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
