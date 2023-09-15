using Ejercicio_ordenadores.Builders.Componentes;
using System.ComponentModel.DataAnnotations;

namespace TiendaOrdenadoresAPI.Models
{
    public class Componente
    {
        public int Id { get; set; }
        [Required]
        public EnumTipoComponentes TipoComponente { get; set; }

        [Required, StringLength(100, ErrorMessage = "La descripcion no puede tener más de 100 caracteres", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; } = "";

        [Required, StringLength(100, ErrorMessage = "El nombre de serie no puede tener más de 100 caracteres", MinimumLength = 1)]
        public string? Serie { get; set; } = "";

        [Required, Range(1, 1000, ErrorMessage = "El calor debe estar entre 1 y 1000")]
        public int Calor { get; set; }
         public long Megas { get; set; }

        [Required, Range(0, 1000, ErrorMessage = "el numero de cores debe estar entre 0 y 1000")]
        public int Cores { get; set; }

        [Required, Range(1, 1000, ErrorMessage = "El precio debe estar entre 1 y 1000")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }
        public int OrdenadorId { get; set; }
        public virtual Ordenador? Ordenador { get; set; }
    }
}
