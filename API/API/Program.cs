using API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "API de Produtos");

List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto()
{
    Nome = "Notebook",
    Preco = 5000,
    Quantidade = 54
});

produtos.Add(new Produto() 
{
    Nome = "Mouse",
    Preco = 100,
    Quantidade = 120
});

produtos.Add(new Produto()
{
    Nome = "Teclado",
    Preco = 450.45,
    Quantidade = 24
});

app.MapGet("/produto/listar", () => 
{
    return produtos;
});

// Criar uma funcionalidade para receber informação
app.Run();
