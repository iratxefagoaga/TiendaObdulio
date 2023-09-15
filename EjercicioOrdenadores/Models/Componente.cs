using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio_ordenadores.Models;

[Index("OrdenadorId", Name = "IX_Componentes_OrdenadorId")]
public partial class Componente
{
    [Key]
    public int Id { get; set; }

    public int TipoComponente { get; set; }

    public string? Descripcion { get; set; } = null!;

    public string? Serie { get; set; } = null!;

    public int Calor { get; set; }

    public long Megas { get; set; }

    public int Cores { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Precio { get; set; }

    public int OrdenadorId { get; set; }

    [ForeignKey("OrdenadorId")]
    [InverseProperty("Componentes")]
    public virtual Ordenador Ordenador { get; set; } = null!;
}
