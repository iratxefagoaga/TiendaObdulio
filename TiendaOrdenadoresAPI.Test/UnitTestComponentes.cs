using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresAPI.Controllers;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services;
using TiendaOrdenadoresAPI.Services.FakeRepositories;

namespace TiendaOrdenadoresAPI.Test
{
    [TestClass]
    public class UnitTestComponentes
    {
        readonly ComponentesController _controlador = new(new FakeRepositoryComponente());
        private readonly OverridenEquals _comparador = new();

        [TestMethod]
        public void GetComponentes()
        {
            List<Componente> expectedListaComponentes = new()
            {
                new Componente()
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
                },
                new Componente()
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
                },
                new Componente()
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
                }
            };
            var result = _controlador.GetComponentes();
            IEnumerable<Componente>? listaComponentes = result.Value;
            
            Assert.IsNotNull(listaComponentes);
            var componentes = listaComponentes.ToList();
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[0], componentes[0]));
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[1], componentes[1]));
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[2], componentes[2]));
        }

        [TestMethod]
        public void GetComponenteBien()
        {
            var result = _controlador.GetComponente(2);
            Componente? componente = _controlador.GetComponente(2).Value;
            Assert.IsNotNull(componente);
            Componente expectedComponente = new()
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
            };
            Assert.IsTrue(_comparador.ComponentesIguales(componente, expectedComponente));
        }

        [TestMethod]
        public void GetComponenteMal()
        {
            var result = _controlador.GetComponente(50);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostComponenteBien()
        {
            Componente componente = new()
            {
                Id = 4,
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente añadido",
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
            };
            var result = _controlador.PostComponente(componente);
            Assert.IsNotNull(result);
            var obtenerComponente = _controlador.GetComponente(4).Value;
            Assert.IsNotNull(obtenerComponente);
            Assert.IsTrue(_comparador.ComponentesIguales(componente, obtenerComponente));
        }
        [TestMethod]
        public void PostComponenteMal()
        {
            Componente componente = new()
            {
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente añadido",
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
            };
            var result = _controlador.PostComponente(componente);
            Assert.IsNotNull(result);
            var obtenerComponente = _controlador.GetComponente(4).Value;
            Assert.IsNull(obtenerComponente);
            }

        [TestMethod]
        public void PutComponente()
        {
            Componente componente = new()
            {
                Id = 1,
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente cambiado",
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
            };
            var componenteAntiguo = _controlador.GetComponente(componente.Id).Value;
            Assert.IsNotNull(componenteAntiguo);
            var result = _controlador.PutComponente(componente.Id, componente);
            Assert.IsNotNull(result);
            var componenteModificado = _controlador.GetComponente(componente.Id).Value;
            Assert.IsNotNull(componenteModificado);
            Componente expectedComponente = new()
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
            };
            Assert.IsFalse(_comparador.ComponentesIguales(componenteModificado, expectedComponente));

        }

        [TestMethod]
        public void DeleteComponenteBien()
        {
            var exists = _controlador.GetComponente(2).Value;
            Assert.IsNotNull(exists);
            var result = _controlador.DeleteComponente(2);
            Assert.IsNotNull(result);
            var noexists = _controlador.GetComponente(2).Result;
            Assert.IsInstanceOfType(noexists, typeof(NotFoundResult));
        }
    }
}