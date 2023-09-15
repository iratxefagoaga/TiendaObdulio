using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejercicio_ordenadores.Models;

public partial class Ordenador
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("Ordenador")]
    public virtual ICollection<Componente> Componentes { get; set; } = new List<Componente>();
}
