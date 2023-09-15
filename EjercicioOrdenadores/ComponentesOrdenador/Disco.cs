namespace Ejercicio_ordenadores.ComponentesOrdenador
{
    public class Disco : IMedible, IClonable
    {
        public Disco(string? descripcion, long megas, decimal coste, int calor, string? numeroSerie)
        {
            Descripcion = descripcion; 
            Megas = megas;
            Coste = coste;
            Calor = calor;
            NumeroSerie = numeroSerie;
            Cores = 0;
            TipoComponente = EnumTipoComponentes.AlmacenamientoPrimario;
        }

        public Disco()
        {
        }

        public string? Descripcion { get; set; }
        public long Megas { get; set; }

        public decimal Coste { get; set; }

        public int Calor { get; set; }

        public string? NumeroSerie { get; set; }
        public string? Id => NumeroSerie;

        public int Cores { get; set; }
        public EnumTipoComponentes TipoComponente { get; set; }

        public decimal GetPrecio()
        {
            return Coste;
        }

        public long GetMemoria()
        {
            return Megas;
        } 
        public float GetTemperatura()
        {
            return Calor;
        }

        public IComponente Clone()
        {
            return (Disco)MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            if (obj != null && obj.GetType() != GetType()) return false;
            if (obj is Disco disco1)
                return disco1.NumeroSerie != null && disco1.NumeroSerie.Equals(NumeroSerie);
            return false;
        }
        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }
}
