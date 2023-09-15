using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services
{
    public class OverridenEquals
    {
        public bool ComponentesIguales(Componente? componente1, Componente? componente2)
        {
            if (componente1 == null && componente2 == null)
                return true;
            if (componente1 == null || componente2 == null)
                return false;
            return componente1.Id == componente2.Id 
                   && componente1.Descripcion == componente2.Descripcion 
                   && componente1.OrdenadorId == componente2.OrdenadorId 
                   && componente1.Calor == componente2.Calor 
                   && componente1.Cores == componente2.Cores 
                   && componente1.Megas == componente2.Megas 
                   && componente1.Precio == componente2.Precio 
                   && componente1.Serie == componente2.Serie 
                   && componente1.TipoComponente == componente2.TipoComponente 
                   && OrdenadoresIguales(componente1.Ordenador,componente2.Ordenador);
        }
        public bool OrdenadoresIguales(Ordenador? ordenador1, Ordenador? ordenador2)
        {
            if (ordenador1 == null && ordenador2 == null)
                return true;
            if (ordenador1 == null || ordenador2 == null)
                return false;
            var result = ordenador1.Id == ordenador2.Id
                         && ordenador1.Descripcion == ordenador2.Descripcion
                         && ordenador1.Calor == ordenador2.Calor
                         && ordenador1.Precio == ordenador2.Precio
                         && PedidosIguales(ordenador1.Pedido, ordenador2.Pedido)
                         && ordenador1.PedidoId == ordenador2.PedidoId;
            if (!result) return result;
            var i = 0;
            if (ordenador1.Componentes == null) return result;
            foreach (var componente in ordenador1.Componentes)
            {
                if (ordenador2.Componentes != null)
                    result = ComponentesIguales(componente, ordenador2.Componentes[i]);
                if (!result)
                    return result;
                i++;
            }
            return result;
        }
        public bool PedidosIguales(Pedido? pedido1, Pedido? pedido2)
        {
            if (pedido1 == null && pedido2 == null)
                return true;
            if(pedido1 == null || pedido2 == null)
                return false;
            var result = pedido1.Id == pedido2.Id 
                   && pedido1.Descripcion == pedido2.Descripcion 
                   && pedido1.Calor == pedido2.Calor 
                   && pedido1.Precio == pedido2.Precio 
                   && ClientesIguales( pedido1.Cliente , pedido2.Cliente) 
                   && pedido1.ClienteId == pedido2.ClienteId 
                   && FacturasIguales(pedido1.Factura , pedido2.Factura) 
                   && pedido1.Fecha == pedido2.Fecha;
            if (!result) return result;
            var i = 0;
            if (pedido1.Ordenadores == null) return result;
            foreach (var ordenador in pedido1.Ordenadores)
            {
                if (pedido2.Ordenadores != null)
                    result = OrdenadoresIguales(ordenador, pedido2.Ordenadores[i]);
                if (!result)
                    return result;
                i++;
            }
            return result;
        }
        public bool FacturasIguales(Factura? factura1, Factura? factura2)
        {
            if (factura1 == null && factura2 == null)
                return true;
            if (factura1 == null || factura2 == null)
                return false;
            var result = factura1.Id == factura2.Id 
                   && factura1.Descripcion == factura2.Descripcion 
                   && factura1.Calor == factura2.Calor 
                   && factura1.Precio == factura2.Precio 
                   && factura1.Fecha == factura2.Fecha;
            if (!result) return result;
            var i = 0;
            if (factura1.Pedidos == null) return result;
            foreach (var pedido in factura1.Pedidos)
            {
                if (factura2.Pedidos != null)
                    result = PedidosIguales(pedido, factura2.Pedidos[i]);
                if (!result)
                    return result;
                i++;
            }
            return result;
        }
        public bool ClientesIguales(Cliente? cliente1, Cliente? cliente2)
        {
            if (cliente1 == null && cliente2 == null)
                return true;
            if (cliente1 == null || cliente2 == null)
                return false;
            var result = cliente1.Id == cliente2.Id 
                   && cliente1.Nombre == cliente2.Nombre 
                   && cliente1.Apellido == cliente2.Apellido 
                   && cliente1.CreditCard == cliente2.CreditCard 
                   && cliente1.Email == cliente2.Email 
                   && cliente1.Password == cliente2.Password;
            if (!result) return result;
            var i = 0;
            if (cliente1.Pedidos == null) return result;
            foreach (var pedido in cliente1.Pedidos)
            {
                if (cliente2.Pedidos != null)
                    result = PedidosIguales(pedido, cliente2.Pedidos[i]);
                if (!result)
                    return result;
                i++;
            }
            return result;
        }
    }
}
