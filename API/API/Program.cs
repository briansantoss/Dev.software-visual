using API.Models;
using Microsoft.AspNetCore.Mvc;

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
    if (produtos.Any()) {
        return Results.Ok(produtos);
    }
    return Results.NotFound();
});

// POST: /produto/cadastrar
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto) => 
{
    produtos.Add(produto);
    return Results.Created("", produto);
});

app.MapPost("/produto/remover/{nome}", (string nome) =>
{
    foreach(Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == nome) 
        {
            produtos.Remove(produtoCadastrado);
            return Results.Ok("Remoção realizada com sucesso");
        }
    }
    return Results.NotFound("Produto não cadastrado");
}
);

app.MapGet("/produto/buscar/{nome}", (string nome) =>
{
    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == nome) 
        {
            return Results.Ok(produtoCadastrado);
        }
    }
    return Results.NotFound();
});

app.MapPost("/produto/alterar/{nome}", (string nome, [FromBody] Produto produtoModificado) => 
{
    int index = produtos.FindIndex(produto => produto.Nome == nome);
    if ( index != -1) 
    {
        produtos[index] = produtoModificado;
        return Results.Ok(produtoModificado);
    }
    return Results.NotFound("Produto não cadastrado");
});

// Criar uma funcionalidade para receber informação
app.Run();
