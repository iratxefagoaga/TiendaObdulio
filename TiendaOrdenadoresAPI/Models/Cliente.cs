using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TiendaOrdenadoresAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Nombre { get; set; }
        [StringLength(50)]
        [Required]
        public string? Apellido { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string? CreditCard { get; set; }

        public List<Pedido>? Pedidos { get; set; }
    }
}
