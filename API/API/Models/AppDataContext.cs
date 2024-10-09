using Microsoft.EntityFrameworkCore;

namespace API.Models;

// Implementando herança da classe DbContext
public class AppDataContext : DbContext
{
    // Definindo classes que representarão tabelas
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ecommerce.db");
    }
}
