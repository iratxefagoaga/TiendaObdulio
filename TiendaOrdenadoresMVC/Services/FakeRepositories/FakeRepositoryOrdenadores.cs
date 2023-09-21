using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.FakeRepositories
{
    public class FakeRepositoryOrdenadores : IOrdenadorRepository
    {
        private readonly List<Ordenador> _ordenadores = new();
        private readonly List<int> _pedidosLista = new() { 1, 2, 3, 4 };
        public FakeRepositoryOrdenadores()
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

        public Task Add(Ordenador ordenador)
        {
            if (ordenador.Id == 0)
                throw new Exception();
            ordenador.Id = _ordenadores.Count + 1;
            _ordenadores.Add(ordenador);
            return Task.CompletedTask;
        }

        public Task<List<Ordenador>> All()
        {
            return Task.FromResult(_ordenadores);
        }

        public Task Delete(int id)
        {
            if (GetById(id).Result ==null)
                throw new Exception();
            _ordenadores.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] deleteInputs)
        {
            foreach (var id in deleteInputs)
            {
                _ordenadores.RemoveAll(p => p.Id == id);
            }

            return Task.CompletedTask;
        }

        public Task Edit(Ordenador ordenador)
        {
            var ordenadorAntiguo = GetById(ordenador.Id);
            if (ordenadorAntiguo.Result != null)
            {
                var indice = _ordenadores.IndexOf(ordenadorAntiguo.Result);
                _ordenadores[indice] = ordenador;
            }
            else
            {
                throw new Exception();
            }

            return Task.CompletedTask;
        }

        public Task<Ordenador?> GetById(int? id)
        {
            return Task.FromResult(_ordenadores.Find(p => p.Id == id));
        }


        public SelectList PedidosLista(Ordenador? ordenador = null)
        {
            return new SelectList(_pedidosLista);
        }
    }
}
