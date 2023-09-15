using Ejercicio_ordenadores.Builders.Componentes;
using Ejercicio_ordenadores.Interfaces;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Models
{
    public class SeedData
    {
        public static async void Initialize(IComponenteRepository componenteRepository, IOrdenadorRepository ordenadorRepository,
            IPedidoRepository pedidoRepository, IFacturasRepository facturasRepository,
            IClienteRepository clienteRepository)
        {
            //Look for facturas
            if (facturasRepository.All().Result.Any())
            {
                return;
            }

            var facturas = new Factura[]
            {
                new()
                {
                    Descripcion = "Factura A",
                    Fecha = DateTime.UtcNow
                },
                new()
                {
                    Descripcion = "Factura B",
                    Fecha = DateTime.UtcNow
                },
                new()
                {
                    Descripcion = "Factura C",
                    Fecha = DateTime.UtcNow
                }
            };

            foreach (var factura in facturas)
            {
                await facturasRepository.Add(factura);
            }

            var clientes = new Cliente[]
            {
                new()
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    CreditCard = "1234567894561124",
                    Email = "juanperez@gmail.com",
                    Password = "contraseñaJuan"
                },
                new()
                {
                    Nombre = "Maria",
                    Apellido = "Lopez",
                    CreditCard = "1245312456315232",
                    Email = "marialopez@gmail.com",
                    Password = "contraseñaMaria"
                },new()
                {
                    Nombre = "Mario",
                    Apellido = "Fernandez",
                    CreditCard = "1234567894561124",
                    Email = "mariofernandez@gmail.com",
                    Password = "contraseñaMario"
                }
            };
            foreach (var cliente in clientes)
            {
                await clienteRepository.Add(cliente);
            }
            

            var pedidos = new Pedido[]
            {
                new()
                {
                    ClienteId = clienteRepository.All().Result.Single(c => c.Nombre == "Maria").Id,
                    Descripcion = "Pedido A",
                    FacturaId = facturasRepository.All().Result.Single(f => f.Descripcion == "Factura A").Id,
                    Fecha = DateTime.UtcNow
                },
                new()
                {
                    ClienteId = clienteRepository.All().Result.Single(c => c.Nombre == "Juan").Id,
                    Descripcion = "Pedido B",
                    FacturaId = facturasRepository.All().Result.Single(f => f.Descripcion == "Factura B").Id,
                    Fecha = DateTime.UtcNow
                },
                new()
                {
                    ClienteId = clienteRepository.All().Result.Single(c => c.Nombre == "Mario").Id,
                    Descripcion = "Pedido A",
                    FacturaId = facturasRepository.All().Result.Single(f => f.Descripcion == "Factura C").Id,
                    Fecha = DateTime.UtcNow
                },
                new()
                {
                    ClienteId = clienteRepository.All().Result.Single(c => c.Nombre == "Mario").Id,
                    Descripcion = "Pedido B",
                    FacturaId = facturasRepository.All().Result.Single(f => f.Descripcion == "Factura C").Id,
                    Fecha = DateTime.UtcNow
                }
            };
            foreach (var pedido in pedidos)
            {
                await pedidoRepository.Add(pedido);
            }
            var ordenadores = new List<Ordenador>();
            foreach (var pedidoA in pedidoRepository.All().Result.Where(p => p.Descripcion == "Pedido A").ToList())
            {
                ordenadores.AddRange(new Ordenador[]
                {
                    new()
                    {
                        Descripcion = "Ordenador Maria",
                        PedidoId = pedidoA.Id
                    },
                    new()
                    {
                        Descripcion = "Ordenador Andres",
                        PedidoId = pedidoA.Id
                    }
                });
            }

            foreach (var pedidoB in pedidoRepository.All().Result.Where(p => p.Descripcion == "Pedido B").ToList())
            {
                ordenadores.AddRange(new Ordenador[]
                {
                    new()
                    {
                        Descripcion = "Ordenador Tiburcio II",
                        PedidoId = pedidoB.Id
                    }, 
                    new()
                    {
                        Descripcion = "Ordenador Andres CF",
                        PedidoId = pedidoB.Id
                    }
                });
            }
           
            foreach (var ordenador in ordenadores)
            {
                await ordenadorRepository.Add(ordenador);
            }
            var componentes = new List<Componente>();
            foreach (var ordenadorMaria in (ordenadorRepository.All().Result.Where(o => o.Descripcion == "Ordenador Maria").ToList()))
            {
                componentes.AddRange(new Componente[]
                    {
                        new()
                        {
                            Calor = 10,
                            Cores = 9,
                            Descripcion = "Procesador Intel i7",
                            Megas = 0,
                            Precio = 134,
                            Serie = "789-XCS",
                            TipoComponente = EnumTipoComponentes.Procesador,
                            OrdenadorId = ordenadorMaria.Id
                        },
                        new()
                        {
                            Calor = 10,
                            Cores = 0,
                            Descripcion = "Banco de Memoria SDRAM",
                            Megas = 512,
                            Precio = 100,
                            Serie = "879-FH",
                            TipoComponente = EnumTipoComponentes.MemoriaRAM,
                            OrdenadorId = ordenadorMaria.Id
                        },
                        new()
                        {
                            Calor = 10,
                            Cores = 0,
                            Descripcion = "DiscoDuro SanDisk",
                            Megas = 500000,
                            Precio = 50,
                            Serie = "789-XX",
                            TipoComponente = EnumTipoComponentes.AlmacenamientoPrimario,
                            OrdenadorId = ordenadorMaria.Id
                        }
                    }
                );
            }

            foreach (var ordenadorAndres in (ordenadorRepository.All().Result.Where(o => o.Descripcion == "Ordenador Andres").ToList()))
            {
                componentes.AddRange(new Componente[]
                {
                    new()
                    {
                        Calor = 24,
                        Cores = 0,
                        Descripcion = "Banco de Memoria SDRAM",
                        Megas = 2000,
                        Precio = 150,
                        Serie = "879FH-T",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = ordenadorAndres.Id
                    },
                    new()
                    {
                        Calor = 39,
                        Cores = 0,
                        Descripcion = "DiscoDuro SanDisk",
                        Megas = 2000000,
                        Precio = 128,
                        Serie = "789-XX-3",
                        TipoComponente = EnumTipoComponentes.AlmacenamientoPrimario,
                        OrdenadorId = ordenadorAndres.Id
                    },
                    new()
                    {
                        Calor = 60,
                        Cores = 34,
                        Descripcion = "Procesador Ryzen AMD",
                        Megas = 0,
                        Precio = 278,
                        Serie = "797-X3",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = ordenadorAndres.Id
                    }
                });
            }

            foreach (var ordenadorTiburcio in (ordenadorRepository.All().Result.Where(o => o.Descripcion == "Ordenador Tiburcio II").ToList()))
            {
                componentes.AddRange(new Componente[]
                {
                    new()
                {
                    Calor = 10,
                    Cores = 9,
                    Descripcion = "Procesador Intel i7",
                    Megas = 0,
                    Precio = 134,
                    Serie = "789-XCS",
                    TipoComponente = EnumTipoComponentes.Procesador,
                    OrdenadorId = ordenadorTiburcio.Id
                },
                new()
                {
                    Calor = 10,
                    Cores = 0,
                    Descripcion = "Banco de Memoria SDRAM",
                    Megas = 512,
                    Precio = 100,
                    Serie = "879-FH",
                    TipoComponente = EnumTipoComponentes.MemoriaRAM,
                    OrdenadorId = ordenadorTiburcio.Id
                },
                new()
                {
                    Calor = 10,
                    Cores = 0,
                    Descripcion = "DiscoDuro SanDisk",
                    Megas = 500000,
                    Precio = 50,
                    Serie = "789-XX",
                    TipoComponente = EnumTipoComponentes.AlmacenamientoPrimario,
                    OrdenadorId = ordenadorTiburcio.Id
                },
                new()
                {
                    Calor = 10,
                    Cores = 0,
                    Descripcion = "Disco Externo Sam",
                    Megas = 9000000,
                    Precio = 134,
                    Serie = "1789-XCS",
                    TipoComponente = EnumTipoComponentes.AlmacenamientoSecunadario,
                    OrdenadorId = ordenadorTiburcio.Id
                },
                new()
                {
                    Calor = 35,
                    Cores = 0,
                    Descripcion = "Disco Mecanico Patatin",
                    Megas = 250,
                    Precio = 37,
                    Serie = "788-FG",
                    TipoComponente = EnumTipoComponentes.AlmacenamientoSecunadario,
                    OrdenadorId = ordenadorTiburcio.Id
                }
                });
            }

            foreach (var ordenadorAndres2 in
                     (ordenadorRepository.All().Result.Where(o => o.Descripcion == "Ordenador Andres CF").ToList()))
            {
                componentes.AddRange(new Componente[]
                {
                    new()
                    {
                        Calor = 24,
                        Cores = 0,
                        Descripcion = "Banco de Memoria SDRAM",
                        Megas = 2000,
                        Precio = 150,
                        Serie = "879FH-T",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = ordenadorAndres2.Id
                    },
                    new()
                    {
                        Calor = 35,
                        Cores = 0,
                        Descripcion = "Disco Mecanico Patatin",
                        Megas = 250,
                        Precio = 37,
                        Serie = "788-FG",
                        TipoComponente = EnumTipoComponentes.AlmacenamientoPrimario,
                        OrdenadorId = ordenadorAndres2.Id
                    },
                    new()
                    {
                        Calor = 39,
                        Cores = 0,
                        Descripcion = "DiscoDuro SanDisk",
                        Megas = 2000000,
                        Precio = 128,
                        Serie = "789-XX-3",
                        TipoComponente = EnumTipoComponentes.AlmacenamientoSecunadario,
                        OrdenadorId = ordenadorAndres2.Id
                    },
                    new()
                    {
                        Calor = 60,
                        Cores = 34,
                        Descripcion = "Procesador Ryzen AMD",
                        Megas = 0,
                        Precio = 278,
                        Serie = "797-X3",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = ordenadorAndres2.Id
                    }
                });
            }
           
            foreach (var componente in componentes)
            {
                await componenteRepository.Add(componente);
            }
        }

        public static Componente ConvetirAComponenteModelo(
            IComponente componente1)
        {
            if (componente1 is not { Descripcion: not null, NumeroSerie: not null }) return new Componente();
            Componente componente = new ()
            {
                Calor = componente1.Calor,
                Cores = componente1.Cores,
                Descripcion = componente1.Descripcion,
                Megas = componente1.Megas,
                Precio = componente1.Coste,
                Serie = componente1.NumeroSerie,
                TipoComponente = componente1.TipoComponente,
            };
            return componente;

        }
    }
}
