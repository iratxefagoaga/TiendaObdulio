namespace Ejercicio_ordenadores.ComponentesOrdenador
{
    public class DiscoSecundario : Disco
    {
        public DiscoSecundario(string? descripcion, long megas, decimal coste, int calor, string? numeroSerie)
        {
            Descripcion = descripcion;
            Megas = megas;
            Coste = coste;
            Calor = calor;
            NumeroSerie = numeroSerie;
            Cores = 0;
            TipoComponente = EnumTipoComponentes.AlmacenamientoSecunadario;
        }
    }
}
