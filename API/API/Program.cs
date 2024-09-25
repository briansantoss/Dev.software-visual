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

app.MapDelete("/produto/remover/{nome}", (string nome) =>
{
    Produto? produto = produtos.Find(x => x.Nome.Equals(nome));
    if (produto == null) 
    {
        return Results.NotFound("Produto não cadastrado");
    }
        produtos.Remove(produto);
        return Results.Ok("Remoção realizada com sucesso");
}
);

app.MapGet("/produto/buscar/{nome}", (string nome) =>
{
    Produto? produtoBuscado = produtos.Find(p => p.Nome == nome);

    if (produtoBuscado == null) 
    {
        return Results.NotFound();  
    }
    return Results.Ok(produtoBuscado);   
});

app.MapPut("/produto/alterar/{nome}", (string nome, [FromBody] Produto produtoModificado) => 
{
    int indice = produtos.FindIndex(produto => produto.Nome == nome);
    if (indice == -1) 
    {
        return Results.NotFound("Produto não cadastrado");
    }
    produtos[indice] = produtoModificado;
    return Results.Ok(produtoModificado);
});

// Criar uma funcionalidade para receber informação
app.Run();
