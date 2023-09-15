namespace Ejercicio_ordenadores.Builders.Ordenadores
{
    public class OrdenadorBuilderMejorado : IOrdenadorBuilder
    {
        private readonly IComponenteBuilder _componenteBuilder = new ComponenteBuilder();

        public IOrdenador DameOrdenador(TipoOrdenadores modeloOrdenador)
        {
            IOrdenador ordenadorDevuelto = new OrdenadorMejorado();
            ValidadorOrdenadorMejoradoAttribute validador = new ();
            switch (modeloOrdenador)
            {
                case TipoOrdenadores.OrdenadorAndres:
                {
                    IOrdenadorBuilder ordenadorBuilder = new OrdenadorBuilder();
                    var ordenador = ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
                    IOrdenador ordenadorMejorado = new OrdenadorMejorado((ordenador as Ordenador)?.Procesador,
                        (ordenador as Ordenador)?.Disco, (ordenador as Ordenador)?.Memoria);
                    if (validador.IsValid(ordenadorMejorado))
                        return ordenadorMejorado;
                    return ordenadorDevuelto;
                }
                case TipoOrdenadores.OrdenadorMaria:
                {
                    IOrdenadorBuilder ordenadorBuilder = new OrdenadorBuilder();
                    var ordenador = ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorMaria);
                    IOrdenador ordenadorMejorado = new OrdenadorMejorado((ordenador as Ordenador)?.Procesador,
                        (ordenador as Ordenador)?.Disco, (ordenador as Ordenador)?.Memoria);
                    if (validador.IsValid(ordenadorMejorado))
                        return ordenadorMejorado;
                    return ordenadorDevuelto;
                }
                case TipoOrdenadores.OrdenadorAndresCf:
                {
                    IOrdenador ordenadorMejorado = new OrdenadorMejorado(
                        _componenteBuilder.GetProcesador(Procesadores.Procesador797X3),
                        _componenteBuilder.GetDisco(Discos.Disco788Fg),
                        _componenteBuilder.GetMemoria(Memorias.Memoria879Fht),
                        new List<IComponente?>() { _componenteBuilder.GetDisco(Discos.Disco789Xx3) });
                    if (validador.IsValid(ordenadorMejorado))
                        return ordenadorMejorado;
                    return ordenadorDevuelto;
                }
                case TipoOrdenadores.OrdenadorTiburcioIi:
                {
                    IOrdenador ordenadorMejorado = new OrdenadorMejorado(
                        _componenteBuilder.GetProcesador(Procesadores.Procesador789Xcs),
                        _componenteBuilder.GetDisco(Discos.Disco789Xx),
                        _componenteBuilder.GetMemoria(Memorias.Memoria879Fh),
                        new List<IComponente?>()
                        {
                            _componenteBuilder.GetDisco(Discos.Disco788Fg),
                            _componenteBuilder.GetDisco(Discos.Disco1789Xcs)
                        });
                    if (validador.IsValid(ordenadorMejorado))
                        return ordenadorMejorado;
                    return ordenadorDevuelto;
                }
                default: return ordenadorDevuelto;
            }
        }
    }
}
