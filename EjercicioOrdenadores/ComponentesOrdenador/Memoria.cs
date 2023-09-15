namespace Ejercicio_ordenadores.ComponentesOrdenador
{
    public class Memoria : IMedible, IClonable
    {
        public Memoria(string? descripcion, long megas, decimal coste, int calor, string? numeroSerie)
        {
            Descripcion = descripcion;
            Megas = megas;
            Coste = coste;
            Calor = calor;
            NumeroSerie = numeroSerie;
            Cores = 0;
            TipoComponente = EnumTipoComponentes.MemoriaRAM;
        }

        public Memoria()
        {
        }

        public string? Descripcion { get; set; }
        public long Megas { get; set; }
        public int Cores { get; set; }

        public decimal Coste { get; set; }

        public int Calor { get; set; }

        public string? NumeroSerie { get; set; }
        public string? Id => NumeroSerie;

        public EnumTipoComponentes TipoComponente { get; set; }

        public long GetMemoria()
        {
            return Megas;
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
            return (Memoria)MemberwiseClone();
        }
        public override bool Equals(object? obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            if (obj != null && obj.GetType() != GetType()) return false;

            return (obj as Memoria)?.NumeroSerie == NumeroSerie;
        }
        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }
}
