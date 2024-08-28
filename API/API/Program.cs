var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Funcionalidade 1");
app.MapGet("/segundafunc", () => "Funcionalidade 2");
app.MapGet("/retornarendereco", () => {
    dynamic endereco = new {
        cep = "82100-741",
        rua = "Parigot De Souza",
        numero = 5300
    };
    return endereco;
} );


// Criar um endpoint pare receber informações
// - 
app.Run();
