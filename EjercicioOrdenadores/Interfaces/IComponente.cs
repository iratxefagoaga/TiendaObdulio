namespace Ejercicio_ordenadores.Interfaces
{
    public interface IComponente : ICosteable, ITemperatura
    {
        int Calor { get; set; }
        int Cores { get; set; }
        string? Descripcion { get; set; }
        long Megas { get; set; }
        decimal Coste { get; set; }
        string? NumeroSerie { get; set; }
        EnumTipoComponentes TipoComponente { get; set; }
    }
}
