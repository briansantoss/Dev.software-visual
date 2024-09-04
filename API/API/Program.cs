using API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.MapGet("/", () => "API de Produtos");

// GET: /produto/listar
app.MapGet("/produto/listar", () => 
{
    return Results.Ok(produtos);
});

// POST: /produto/cadastrar
app.MapPost("/produto/cadastrar/{nome}", (string nome) => 
{
    Produto produto = new Produto();
    produto.Nome = nome;
    produtos.Add(produto);

    return Results.Ok(produtos);
});

// Criar uma funcionalidade para receber informação
app.Run();
