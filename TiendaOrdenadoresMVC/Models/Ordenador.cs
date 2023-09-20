using System.ComponentModel.DataAnnotations;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Models
{
    public class Ordenador : IEntity
    {
        public int Id { get; set; }

        [Required,
         StringLength(100, ErrorMessage = "La descripcion no puede tener más de 100 caracteres", MinimumLength = 1)]
        public string? Descripcion { get; set; } = "";

        public virtual List<Componente>? Componentes { get; set; }
        public int PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }
        [DataType(DataType.Currency)]
        public decimal Precio => Componentes?.Sum(c => c.Precio) ?? 0;
        public int Calor => Componentes?.Sum(c => c.Calor) ?? 0;
    }
}
