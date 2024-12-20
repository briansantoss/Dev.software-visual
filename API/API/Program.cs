using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options => {
    options.AddPolicy("Acesso Total", 
        configs => configs.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.MapGet("/", () => "API de Produtos");

// GET: /categoria/listar
app.MapGet("/categoria/listar", ([FromServices] AppDataContext context) => 
{
    // Posso usar o método Count()
    if (context.Categorias.Any()) 
    {
        return Results.Ok(context.Categorias.ToList());
    }
    return Results.NotFound();
});

// POST: /categoria/cadastrar
app.MapPost("/categoria/cadastrar", ([FromBody] Categoria categoria, [FromServices] AppDataContext context) => 
{
    context.Categorias.Add(categoria);
    context.SaveChanges();
    return Results.Created("", categoria);
});

// GET: /produto/listar
app.MapGet("/produto/listar", ([FromServices] AppDataContext context) => 
{
    // Posso usar o método Count()
    if (context.Produtos.Any()) {
        // Posso passar também a string da propriedade de navegação
        return Results.Ok(context.Produtos.Include(p => p.Categoria).ToList());
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
});

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

    // Modificando atributos/informações a respeito do produto
    produtoBuscado.Nome = produtoModificado.Nome;
    produtoBuscado.Descricao = produtoModificado.Descricao;
    produtoBuscado.Preco = produtoModificado.Preco;
    produtoBuscado.Quantidade = produtoModificado.Quantidade;

    context.SaveChanges();
    return Results.Ok(produtoModificado);
});

app.UseCors("Acesso Total");
app.Run();