using Ejercicio_ordenadores.ComponentesOrdenador;
using Ejercicio_ordenadores.Models;

namespace Ejercicio_ordenadores.App_Data
{
    public class DataBase
    {
        private readonly AppDbContext _contexto = new ();

        public void AñadirComponenteDataBase(Componente componente)
        {
            _contexto.Componentes?.Add(componente);
            _contexto.SaveChanges();
        }
        public IComponente? ObtenerComponente(int id)
        {
            var componente = _contexto.Componentes?.Find(id);
            return componente != null ? ConvertirComponente(componente) : null;
        }

        public static IComponente? ConvertirComponente(Componente componenteModelo)
        {
            var tipoComponente = (EnumTipoComponentes)componenteModelo.TipoComponente;
            IComponente? componente = null;
            if (tipoComponente == EnumTipoComponentes.AlmacenamientoPrimario)
                componente = new Disco(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio,
                    componenteModelo.Calor, componenteModelo.Serie);
            else if (tipoComponente == EnumTipoComponentes.MemoriaRAM)
                componente = new Memoria(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio,
                    componenteModelo.Calor, componenteModelo.Serie);
            else if(tipoComponente == EnumTipoComponentes.Procesador)
                componente = new Procesador(componenteModelo.Descripcion, componenteModelo.Cores, componenteModelo.Precio,
                    componenteModelo.Calor, componenteModelo.Serie);
            else if(tipoComponente == EnumTipoComponentes.AlmacenamientoSecunadario)
                componente = new DiscoSecundario(componenteModelo.Descripcion, componenteModelo.Megas, componenteModelo.Precio,
                    componenteModelo.Calor, componenteModelo.Serie);
            var validador = new ValidadorComponentesAttribute();
            return validador.IsValid(componente) ? componente : null;
        }

    }
}
