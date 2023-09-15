namespace Ejercicio_ordenadores.Builders.Ordenadores
{
    public class OrdenadorBuilder : IOrdenadorBuilder
    {
        private readonly IComponenteBuilder _componenteBuilder = new ComponenteBuilder();
        public IOrdenador DameOrdenador(TipoOrdenadores modeloOrdenador)
        {
            IOrdenador ordenador = new Ordenador();
            switch (modeloOrdenador)
            {
                case TipoOrdenadores.OrdenadorMaria:
                    {
                        return new Ordenador(_componenteBuilder.GetProcesador(Procesadores.Procesador789Xcs),
                            _componenteBuilder.GetDisco(Discos.Disco789Xx),
                            _componenteBuilder.GetMemoria(Memorias.Memoria879Fh));
                    }
                case TipoOrdenadores.OrdenadorAndres:
                    {
                        return new Ordenador(_componenteBuilder.GetProcesador(Procesadores.Procesador797X3),
                            _componenteBuilder.GetDisco(Discos.Disco789Xx3),
                            _componenteBuilder.GetMemoria(Memorias.Memoria879Fht));
                    }
                default: return ordenador;
            }
        }
    }
}
