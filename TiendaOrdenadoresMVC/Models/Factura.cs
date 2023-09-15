using System.ComponentModel.DataAnnotations;

namespace MVC_ComponentesCodeFirst.Models
{
    public class Factura
    {
        private DateTime _fecha;
        public int Id { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Fecha
        {
            get => _fecha;
            set => _fecha = value;
        }
        public virtual List<Pedido>? Pedidos { get; set; }
        [DataType(DataType.Currency)]
        public decimal Precio => Pedidos?.Sum(o => o.Precio) ?? 0;
        public int Calor => Pedidos?.Sum(c => c.Calor) ?? 0;
    }
}
