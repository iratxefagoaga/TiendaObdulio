using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.FakeRepositories
{
    public class FakeRepositoryOrdenador:IOrdenadorRepository
    {
        private readonly List<Ordenador> _ordenadores = new();
        private readonly List<int> _pedidosLista = new() { 1, 2, 3, 4 };
        public FakeRepositoryOrdenador()
        {
            _ordenadores.Add(new Ordenador()
            {
                Id = 1,
                Descripcion = "Ordenador 1",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new ()
                    {
                        Id = 1,
                        Calor = 12,
                        Cores = 0,
                        Descripcion = "Memoria Ram 1",
                        Megas = 30,
                        Precio = 500,
                        Serie = "ABC2",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 1
                    },
                    new ()
                    {
                        Id = 2,
                        Calor = 15,
                        Cores = 12,
                        Descripcion = "Procesador i5",
                        Megas = 0,
                        Precio = 60,
                        Serie = "ABC3",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 1
                    }
                },
                PedidoId = 1
            });
            _ordenadores.Add(new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador 2",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new ()
                    {
                        Id = 1,
                        Calor = 15,
                        Cores = 0,
                        Descripcion = "Memoria Ram 2",
                        Megas = 64,
                        Precio = 500,
                        Serie = "413",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 2
                    },
                    new ()
                    {
                        Id = 2,
                        Calor = 43,
                        Cores = 12,
                        Descripcion = "Procesador i7",
                        Megas = 0,
                        Precio = 122,
                        Serie = "2342",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 2
                    }
                },
                PedidoId = 2
            });
        }

        public void Add(Ordenador ordenador)
        {
            ordenador.Id = _ordenadores.Count + 1;
            _ordenadores.Add(ordenador);
        }

        public void Delete(int id)
        {
            _ordenadores.RemoveAll(p => p.Id == id);
        }
        public void Edit(Ordenador ordenador)
        {
            var ordenadorAntiguo = GetById(ordenador.Id);
            if (ordenadorAntiguo != null)
            {
                var indice = _ordenadores.IndexOf(ordenadorAntiguo);
                _ordenadores[indice] = ordenador;
            }
            else
            {
                throw new Exception();
            }
        }

        public Ordenador? GetById(int id)
        {
            return _ordenadores.Find(p => p.Id == id);
        }

        public List<Ordenador> All()
        {
            return _ordenadores;
        }
    }
}
