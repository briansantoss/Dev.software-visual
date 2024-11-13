namespace API.Models;

public class Produto
{
    public Produto() 
    {
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;
    }
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set;}
    public DateTime CriadoEm { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}
