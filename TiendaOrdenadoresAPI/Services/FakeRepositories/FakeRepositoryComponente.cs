using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.FakeRepositories
{
    public class FakeRepositoryComponente : IComponenteRepository
    {
        private readonly List<Componente> _componentes = new();
        private readonly List<int> _ordenadoresLista = new() { 1, 2, 3, 4 };

        public FakeRepositoryComponente()
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
        public void Add(Componente componente)
        {
            _componentes.Add(componente);
        }

        public List<Componente> All()
        {
            return _componentes;
        }

        public void Delete(int id)
        {
            _componentes.RemoveAll(p => p.Id == id);
        }

        public void Edit(Componente componente)
        {
            var componenteAntiguo = GetById(componente.Id) ?? throw new Exception();
            var indice = _componentes.IndexOf(componenteAntiguo);
            _componentes[indice] = componente;
        }

        public Componente? GetById(int id)
        {
            return _componentes.Find(p => p.Id == id);
        }
    }
}
