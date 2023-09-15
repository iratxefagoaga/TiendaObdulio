using Ejercicio_ordenadores.ComponentesOrdenador;

namespace Ejercicio_ordenadores.Ordenadores
{
    public class Ordenador : IOrdenador
    {
        public Ordenador()
        {

        }
        public Ordenador(IComponente? procesador, IComponente? disco, IComponente? memoria)
        {
            Procesador = procesador;
            Disco = disco;
            Memoria = memoria;
        }

        public IComponente? Procesador { get; set; }

        public IComponente? Disco { get; set; }

        public IComponente? Memoria { get; set; }
        public string Id =>
            (Disco as Disco)?.NumeroSerie + (Procesador as Disco)?.NumeroSerie +
            (Memoria as Memoria)?.NumeroSerie;

        public override bool Equals(object? obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            if (obj?.GetType() != GetType()) return false;
            if(obj is not Ordenador ordenador1) return false;
            // Return true if  x and y fields match.
            return ordenador1.Procesador != null && ordenador1 is { Memoria: not null, Disco: not null } && ordenador1.Disco.Equals(Disco) && ordenador1.Memoria.Equals(Memoria) && ordenador1.Procesador.Equals(Procesador);
        }

        public virtual decimal GetPrecio()
        {
            if (Procesador != null && Memoria != null && Disco != null)
                return Procesador.GetPrecio() + Memoria.GetPrecio() + Disco.GetPrecio();
            return 0;
        }

        public virtual float GetTemperatura()
        {
            if (Procesador != null && Memoria != null && Disco !=null)
                return Procesador.GetTemperatura() + Memoria.GetTemperatura() + Disco.GetTemperatura();
            return 0;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
