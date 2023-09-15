namespace Ejercicio_ordenadores.ComponentesOrdenador
{
    public class Procesador : IProcesable, IClonable
    {
        public Procesador(string? descripcion, int cores, decimal coste, int calor, string? numeroSerie)
        {
            Descripcion = descripcion;
            Cores = cores;
            Coste = coste;
            Calor = calor;
            NumeroSerie = numeroSerie;
            Megas = 0;
            TipoComponente = EnumTipoComponentes.Procesador;
        }

        public Procesador()
        {
        }

        public string? Descripcion { get; set; }
        public int Cores { get; set; }

        public decimal Coste { get; set; }

        public int Calor { get; set; }

        public string? NumeroSerie { get; set; }
        public string? Id=> NumeroSerie;

        public long Megas { get; set ; }
        public EnumTipoComponentes TipoComponente { get; set; }

        public int GetCores()
        {
            return Cores;
        }

        public decimal GetPrecio()
        {
            return Coste;
        }

        public float GetTemperatura()
        {
            return Calor;
        }
        public IComponente Clone()
        {
            return (Procesador)MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            if (obj != null && obj.GetType() != GetType()) return false;

            return (obj as Procesador)?.NumeroSerie == NumeroSerie;
        }

        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }
}
