using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
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
app.MapGet("/produto/listar", ([FromServices] AppDataContext context) => 
{
    // Posso usar o método Count()
    if (context.Produtos.Any()) {
        return Results.Ok(context.Produtos.ToList());
    }
    return Results.NotFound();
});

// POST: /produto/cadastrar
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext context) => 
{
    context.Produtos.Add(produto);
    context.SaveChanges();
    return Results.Created("", produto);
});

app.MapDelete("/produto/remover/{id}", ([FromRoute] string id, [FromServices] AppDataContext context) =>
{
    Produto? produtoBuscado = context.Produtos.Find(id);  
    if (produtoBuscado == null) 
    {
        return Results.NotFound("Produto não cadastrado");
    }
        context.Produtos.Remove(produtoBuscado);
        context.SaveChanges();
        return Results.Ok("Remoção realizada com sucesso");
}
);

app.MapGet("/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext context) =>
{
    // Busca por outros parâmetros além do id
    // Produto? produto = context.Produtos.FirstOrDefault(x => x.Id == id);
    Produto? produtoBuscado = context.Produtos.Find(id);
    if (produtoBuscado == null) 
    {
        return Results.NotFound();  
    }
    return Results.Ok(produtoBuscado);   
});

app.MapPut("/produto/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produtoModificado, [FromServices] AppDataContext context) => 
{
    Produto? produtoBuscado = context.Produtos.Find(id);
    if (produtoBuscado == null) 
    {
        return Results.NotFound();  
    }
    produtoBuscado = produtoModificado;
    context.SaveChanges();
    return Results.Ok(produtoModificado);
});

// Criar uma funcionalidade para receber informação
app.Run();
