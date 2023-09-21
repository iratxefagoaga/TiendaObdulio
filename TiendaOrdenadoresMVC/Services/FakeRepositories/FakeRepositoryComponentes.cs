using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.FakeRepositories
{
    public class FakeRepositoryComponentes : IComponenteRepository
    {
        private readonly List<Componente> _componentes = new();
        private readonly List<int> _ordenadoresLista = new() { 1, 2, 3, 4 };

        public FakeRepositoryComponentes()
        {
            _componentes.Add(new Componente()
            {
                Id = 1,
                Calor = 10,
                Cores = 12,
                Descripcion = "Procesador i7",
                Megas = 0,
                Precio = 130,
                Serie = "ABC1",
                TipoComponente = EnumTipoComponentes.Procesador,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            });
            _componentes.Add(new Componente()
            {
                Id = 2,
                Calor = 12,
                Cores = 0,
                Descripcion = "Memoria Ram 1",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            });
            _componentes.Add(new Componente()
            {
                Id = 3,
                Calor = 15,
                Cores = 12,
                Descripcion = "Procesador i5",
                Megas = 0,
                Precio = 60,
                Serie = "ABC3",
                TipoComponente = EnumTipoComponentes.Procesador,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            });
        }

        public Task Add(Componente componente)
        {
            if (componente.Id == 0)
            {
                throw new Exception("Componente no valido");
            }
            _componentes.Add(componente);
            return Task.CompletedTask;
        }

        public Task<List<Componente>> All()
        {
            return Task.FromResult(_componentes);
        }

        public Task Delete(int id)
        {
            if (GetById(id).Result == null)
                throw new Exception();
            _componentes.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] deleteInputs)
        {
            foreach (var id in deleteInputs)
            {
                _componentes.RemoveAll(p => p.Id == id);
            }

            return Task.CompletedTask;
        }

        public Task Edit(Componente componente)
        {
            var componenteAntiguo = GetById(componente.Id);
            if (componenteAntiguo.Result == null) throw new Exception();
            var indice = _componentes.IndexOf(componenteAntiguo.Result);
            _componentes[indice] = componente;
            return Task.CompletedTask;
        }

        public Task<Componente?> GetById(int? id)
        {
            return Task.FromResult(_componentes.Find(p => p.Id == id));
        }

        public SelectList OrdenadoresLista(int componenteId = 0)
        {
            return new SelectList(_ordenadoresLista);
        }
    }
}
