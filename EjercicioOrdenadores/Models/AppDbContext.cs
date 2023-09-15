using Microsoft.EntityFrameworkCore;

namespace Ejercicio_ordenadores.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Componente>? Componentes { get; set; }

    public virtual DbSet<Ordenador>? Ordenadores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var s = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            if (s != null)
            {
                var name = Directory.GetParent(s)?.FullName;
                if (name != null)
                {
                    var fullName = Directory.GetParent(name)?.FullName;
                    if (fullName != null)
                    {
                        var path = fullName;
                        var connectionString =
                            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=[DataDirectory]\\App_Data\\MVC_ComponentesCodeFirst.mdf;Integrated Security=True";
                        optionsBuilder.UseSqlServer(connectionString.Replace("[DataDirectory]", path));
                    }
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
