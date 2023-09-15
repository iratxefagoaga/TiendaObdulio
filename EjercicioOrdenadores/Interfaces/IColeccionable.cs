using Ejercicio_ordenadores.Builders.Ordenadores;

namespace Ejercicio_ordenadores.Interfaces
{
    public interface IColeccionable
    {
        public void AñadirOrdenador(TipoOrdenadores modeloOrdenador);
        public void QuitarOrdenador(TipoOrdenadores modeloOrdenador);
    }
}
