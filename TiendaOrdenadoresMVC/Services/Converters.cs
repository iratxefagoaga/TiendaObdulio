using Ejercicio_ordenadores.Builders.Componentes;
using Ejercicio_ordenadores.Interfaces;
using MVC_ComponentesCodeFirst.Models;
using Ejercicio_ordenadores.ComponentesOrdenador;
using Ejercicio_ordenadores.Ordenadores;
using Ordenador = MVC_ComponentesCodeFirst.Models.Ordenador;

namespace MVC_ComponentesCodeFirst.Services
{
    public class Converters
    {
        public IComponente? ConvertirComponente(Componente componenteModelo)
        {
            IComponente? componente = null;
            if (componenteModelo.TipoComponente == EnumTipoComponentes.AlmacenamientoPrimario)
                componente = new Disco(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio,componenteModelo.Calor, componenteModelo.Serie);
            else if (componenteModelo.TipoComponente == EnumTipoComponentes.MemoriaRAM)
                componente = new Memoria(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio, componenteModelo.Calor, componenteModelo.Serie);
            else if (componenteModelo.TipoComponente == EnumTipoComponentes.Procesador)
                componente = new Procesador(componenteModelo.Descripcion, componenteModelo.Cores, componenteModelo.Precio, componenteModelo.Calor, componenteModelo.Serie);
            else if (componenteModelo.TipoComponente == EnumTipoComponentes.AlmacenamientoSecunadario)
                componente = new DiscoSecundario(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio, componenteModelo.Calor, componenteModelo.Serie);

            var validador = new ValidadorComponentesAttribute();
            return validador.IsValid(componente) ? componente : null;
        }

        public IOrdenador? ConvertirOrdenador(Ordenador ordenadorModelo)
        {
            if (ordenadorModelo.Componentes == null) return null;
            IComponente? procesador = null;
            IComponente? ram = null;
            IComponente? disco = null;
            List<IComponente?> discosSecundarios = new();
            foreach (var componente in ordenadorModelo.Componentes)
            {
                switch (componente.TipoComponente)
                {
                    case EnumTipoComponentes.Procesador:
                        procesador = ConvertirComponente(componente);
                        break;
                    case EnumTipoComponentes.MemoriaRAM:
                        ram = ConvertirComponente(componente);
                        break;
                    case EnumTipoComponentes.AlmacenamientoPrimario:
                        disco = ConvertirComponente(componente);
                        break;
                    case EnumTipoComponentes.AlmacenamientoSecunadario:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (componente.TipoComponente == EnumTipoComponentes.AlmacenamientoPrimario)
                    discosSecundarios.Add(ConvertirComponente(componente));
                if (procesador == null || ram == null || disco == null)
                {
                    return null;
                }
            }
            IOrdenador ordenador = new OrdenadorMejorado(procesador, ram, disco, discosSecundarios);

            var validador = new ValidadorOrdenadorMejoradoAttribute();
            return validador.IsValid(ordenador) ? ordenador : null;

        }
    }
}
