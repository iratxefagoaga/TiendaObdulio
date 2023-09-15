using Ejercicio_ordenadores.Builders.Ordenadores;

namespace Ejercicio_ordenadores.Pedidos
{
    public class Pedido : IColeccionable, ICosteable, ITemperatura
    {
        private readonly List<IOrdenador>? _listaOrdenadores = new ();
        private decimal _precio;
        private float _temperatura;
        private readonly IOrdenadorBuilder _builderOrdenador = new OrdenadorBuilderMejorado();

        public List<IOrdenador>? ListaOrdenadores => _listaOrdenadores;

        public void AñadirOrdenador(TipoOrdenadores modeloOrdenador)
        {
            var ordenador = _builderOrdenador.DameOrdenador(modeloOrdenador);
            _listaOrdenadores?.Add(ordenador);
            _precio+=ordenador.GetPrecio();
            _temperatura += ordenador.GetTemperatura();
        }

        public decimal GetPrecio()
        {
            return _precio;
        }

        public float GetTemperatura()
        {
            return _temperatura;
        }

        public void QuitarOrdenador(TipoOrdenadores modeloOrdenador)
        {
            var ordenador = _builderOrdenador.DameOrdenador(modeloOrdenador);
            if (_listaOrdenadores != null && _listaOrdenadores.Contains(ordenador))
            {
                _listaOrdenadores.Remove(ordenador);
                _precio -= ordenador.GetPrecio();
                _temperatura -= ordenador.GetTemperatura();
            }
        }
    }
}
