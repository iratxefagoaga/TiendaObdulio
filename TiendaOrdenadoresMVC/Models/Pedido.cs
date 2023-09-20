using System.ComponentModel.DataAnnotations;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Models
{
    public class Pedido : IEntity
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

        public virtual List<Ordenador>? Ordenadores { get; set; }
        [DataType(DataType.Currency)]
        public decimal Precio => Ordenadores?.Sum(o => o.Precio) ?? 0;
        public int Calor => Ordenadores?.Sum(c => c.Calor) ?? 0;
        public int FacturaId { get; set; }
        public virtual Factura? Factura { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get;set; }
    }
}
