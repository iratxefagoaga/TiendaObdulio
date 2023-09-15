namespace Ejercicio_ordenadores.Ordenadores
{
    public class OrdenadorMejorado : Ordenador
    {
        public OrdenadorMejorado() { }
        public OrdenadorMejorado(IComponente? procesador, IComponente? discoPrimario, IComponente? memoria,
            List<IComponente?>? discosSecundarios = null) : base(procesador, discoPrimario, memoria)
        {
            if(discosSecundarios != null)
                DiscosSecundarios = discosSecundarios;
        }

        public void AñadirDiscosSecundarios(IComponente? disco)
        {
            DiscosSecundarios?.Add(disco);
        }

        public void QuitarDiscoSecundario(IComponente? disco)
        {
            if(DiscosSecundarios != null && DiscosSecundarios.Contains(disco))
                DiscosSecundarios.Remove(disco);
        }
        public List<IComponente?>? DiscosSecundarios { get; set; } = new();

        public override decimal GetPrecio()
        {
            decimal precioDiscos = 0;
            if (DiscosSecundarios != null)
                foreach (var disco in DiscosSecundarios)
                {
                    if (disco != null) precioDiscos += disco.GetPrecio();
                }

            if (Procesador != null && Memoria != null && Disco != null)
                return Procesador.GetPrecio() + Memoria.GetPrecio() + Disco.GetPrecio() + precioDiscos;
            return 0;
        }
        public override float GetTemperatura()
        {
            float temperaturaDiscos = 0;
            if (DiscosSecundarios != null)
                foreach (var disco in DiscosSecundarios)
                {
                    if (disco != null) temperaturaDiscos += disco.GetTemperatura();
                }

            if (Procesador != null && Memoria != null && Disco != null)
                return Procesador.GetTemperatura() + Memoria.GetTemperatura() + Disco.GetTemperatura() +
                       temperaturaDiscos;
            return temperaturaDiscos;
        }

        public List<IComponente?> ComponentesOrdenador()
        {
            List<IComponente?> lista = new () { Disco, Memoria, Procesador };
            if (DiscosSecundarios != null) lista.AddRange(DiscosSecundarios);
            return lista;
        }
    }
}