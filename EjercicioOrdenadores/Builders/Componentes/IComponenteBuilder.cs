namespace Ejercicio_ordenadores.Builders.Componentes
{
    public interface IComponenteBuilder
    {
        public IComponente? GetDisco(Discos modeloDisco);
        public IComponente? GetProcesador(Procesadores modeloProcesador);
        public IComponente? GetMemoria(Memorias modeloMemoria);
    }
}
