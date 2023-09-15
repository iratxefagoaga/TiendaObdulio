namespace Ejercicio_ordenadores.Pedidos
{
    public class Almacen
    {
        private readonly Dictionary<IComponente, int> _stockComponentes = new ();
        public Dictionary<IComponente, int> StockComponentes => _stockComponentes;

        public void AñadirComponente(IComponente? componente, int cuantosComponentes = 1)
        {
            if (componente != null)
            {
                StockComponentes.TryAdd(componente, cuantosComponentes);
            }
        }

        public void QuitarComponente(IComponente? componente, int cuantosComponentes = 1)
        {
            if (ContainsKey(componente, out var numeroDeComponentes))
            {
                if (numeroDeComponentes < cuantosComponentes)
                {
                    throw new Exception("No hay tantos componentes");
                }
                foreach (var componente1 in StockComponentes.Keys)
                {
                    if (componente1.Equals(componente))
                    {
                        if(numeroDeComponentes == cuantosComponentes)
                            StockComponentes.Remove(componente1);
                        else if (numeroDeComponentes > cuantosComponentes)
                            StockComponentes[componente1] -= cuantosComponentes;
                    }
                }
            }
        }

        public bool ContainsKey(IComponente? componente, out int value)
        {
            value = 0;
            foreach (var componente1 in StockComponentes.Keys)
            {
                if (componente1.Equals(componente))
                {
                    value = StockComponentes[componente1];
                    return true;
                }
            }
            return false;
        }
    }
}
