using Microsoft.Data.SqlClient;
using System.Data;
using TiendaOrdenadoresAPI.Logging;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;
using Pedido = TiendaOrdenadoresAPI.Models.Pedido;

namespace TiendaOrdenadoresAPI.Services.Repositories
{
    public class PedidoAdoRepositroy : IGenericRepositoryAdo<Pedido>
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly string? _connectionString = Configuration.GetConnectionString("OrdenadoresContext")
            ?.Replace("[DataDirectory]", Directory.GetCurrentDirectory());

        private readonly ILoggerManager _loggerManager;

        public PedidoAdoRepositroy(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void Add(Pedido pedido)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    $"INSERT INTO Pedidos (Descripcion, Fecha, FacturaId, ClienteId) " +
                    $"VALUES (@Descripcion, @Fecha, @FacturaId, @ClienteId)";
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = pedido.Descripcion;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = pedido.Fecha;
                cmd.Parameters.Add("@FacturaId", SqlDbType.Int).Value = pedido.FacturaId;
                cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = pedido.ClienteId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar pedido. Message: " + ex.Message);
                throw;
            }
        }

        public List<Pedido> All()
        {
            try
            {
                List<Pedido> listaPedidos = new();
                using var con = new SqlConnection(_connectionString);
                var query =
                    "SELECT Pedidos.Id, Pedidos.Descripcion, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId, " +
                    "Facturas.Id AS IdFactura, Facturas.Descripcion AS DescripcionFactura, Facturas.Fecha AS FechaFactura, " +
                    "Clientes.Id AS IdCliente, Clientes.Nombre, Clientes.Apellido, Clientes.Email, Clientes.Password, Clientes.CreditCard " +
                    "FROM Facturas INNER JOIN " +
                    "Pedidos ON Facturas.Id = Pedidos.FacturaId " +
                    "INNER JOIN Clientes ON Pedidos.ClienteId = Clientes.Id";
                var cmd = new SqlCommand(query, con);
                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Descripcion = rdr["Descripcion"].ToString(),
                        Fecha = (DateTime)rdr["Fecha"],
                        FacturaId = (int)rdr["FacturaId"],
                        ClienteId = (int)rdr["ClienteId"],
                        Cliente = new Cliente()
                        {
                            Apellido = rdr["Apellido"].ToString(),
                            CreditCard = rdr["CreditCard"].ToString(),
                            Email = rdr["Email"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Id = (int) rdr["IdCliente"],
                            Nombre = rdr["Nombre"].ToString()
                        },
                        Factura = new Factura()
                        {
                            Id = (int)rdr["IdFactura"],
                            Descripcion = rdr["DescripcionFactura"].ToString(),
                            Fecha = (DateTime)rdr["FechaFactura"]
                        },
                        Ordenadores = ListaOrdenadores((int)rdr["Id"])
                        };
                    listaPedidos.Add(pedido);
                }

                con.Close();

                return listaPedidos;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al obtener lista de pedidos. Message: " + ex.Message);
                throw;
            }
        }

        private List<Ordenador> ListaOrdenadores(int pedidoId)
        {
            List<Ordenador> listaOrdenadores = new();
            using var con = new SqlConnection(_connectionString);
            var query =
                "SELECT Ordenadores.Descripcion, Ordenadores.Id, Ordenadores.PedidoId, " +
                "Pedidos.Id AS IdPedido, Pedidos.Descripcion AS DescripcionPedido, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId " +
                "FROM Ordenadores INNER JOIN  Pedidos ON Ordenadores.PedidoId = Pedidos.Id " +
                "WHERE Ordenadores.PedidoId = " + pedidoId;
            var cmd = new SqlCommand(query, con);
            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var ordenador = new Ordenador
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Descripcion = rdr["Descripcion"].ToString(),
                    PedidoId = (int)rdr["PedidoId"],
                    Pedido = new Pedido()
                    {
                        Id = (int)rdr["IdPedido"],
                        Descripcion = rdr["DescripcionPedido"].ToString(),
                        ClienteId = (int)rdr["ClienteId"],
                        FacturaId = (int)rdr["FacturaId"],
                        Fecha = (DateTime)rdr["Fecha"]
                    },
                    Componentes = ListaComponentes(Convert.ToInt32(rdr["Id"]))
                };
                listaOrdenadores.Add(ordenador);
            }

            con.Close();

            return listaOrdenadores;
        }
        private List<Componente> ListaComponentes(int ordenadorId)
        {
            List<Componente> listaComponentes = new();
            using var con = new SqlConnection(_connectionString);
            var query =
                "SELECT Id, TipoComponente, Serie, Descripcion, Calor, Megas, Cores, Precio, OrdenadorId FROM Componentes WHERE OrdenadorId = " + ordenadorId;
            var cmd = new SqlCommand(query, con);
            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var componente = new Componente
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Calor = Convert.ToInt32(rdr["Calor"]),
                    Cores = Convert.ToInt32(rdr["Cores"]),
                    Descripcion = rdr["Descripcion"].ToString(),
                    Megas = Convert.ToInt64(rdr["Megas"]),
                    OrdenadorId = Convert.ToInt32(rdr["OrdenadorId"]),
                    Precio = (decimal)rdr["Precio"],
                    Serie = rdr["Serie"].ToString(),
                    TipoComponente =
                        (Ejercicio_ordenadores.Builders.Componentes.EnumTipoComponentes)rdr["TipoComponente"]
                };
                listaComponentes.Add(componente);
            }

            con.Close();
            return listaComponentes;
        }

        public void Delete(int id)
        {
            try
            {
                var pedido = GetById(id);
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = $"DELETE FROM Pedidos WHERE Id = {id}";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                _loggerManager.LogDebug(
                    $"Pedido eliminado. Descripcion: {pedido.Descripcion}");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar pedido. Message: " + ex.Message);
                throw;
            }
        }

        public void Edit(Pedido pedido)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    $"UPDATE Pedidos " +
                    $"SET Descripcion =@Descripcion, Fecha = @Fecha, FacturaId = @FacturaId, ClienteId = @ClienteId WHERE Id = {pedido.Id} ";
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = pedido.Descripcion;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = pedido.Fecha;
                cmd.Parameters.Add("@FacturaId", SqlDbType.Int).Value = pedido.FacturaId;
                cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = pedido.ClienteId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar pedido. Message: " + ex.Message);
                throw;
            }
        }

        public Pedido GetById(int id)
        {
            Pedido pedido = new();
            using var con = new SqlConnection(_connectionString);
            var sqlQuery =
                "SELECT Pedidos.Id, Pedidos.Descripcion, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId, " +
                "Facturas.Id AS IdFactura, Facturas.Descripcion AS DescripcionFactura, Facturas.Fecha AS FechaFactura, " +
                "Clientes.Id AS IdCliente, Clientes.Nombre, Clientes.Apellido, Clientes.Email, Clientes.Password, Clientes.CreditCard " +
                "FROM Facturas INNER JOIN " +
                "Pedidos ON Facturas.Id = Pedidos.FacturaId " +
                "INNER JOIN Clientes ON Pedidos.ClienteId = Clientes.Id " +
                "WHERE Pedidos.Id = "+id;
            var cmd = new SqlCommand(sqlQuery, con);
            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                pedido = new Pedido
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Descripcion = rdr["Descripcion"].ToString(),
                    Fecha = (DateTime)rdr["Fecha"],
                    FacturaId = (int)rdr["FacturaId"],
                    ClienteId = (int)rdr["ClienteId"],
                    Cliente = new Cliente()
                    {
                        Apellido = rdr["Apellido"].ToString(),
                        CreditCard = rdr["CreditCard"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Password = rdr["Password"].ToString(),
                        Id = (int)rdr["IdCliente"],
                        Nombre = rdr["Nombre"].ToString()
                    },
                    Factura = new Factura()
                    {
                        Id = (int)rdr["IdFactura"],
                        Descripcion = rdr["DescripcionFactura"].ToString(),
                        Fecha = (DateTime)rdr["FechaFactura"]
                    },
                    Ordenadores = ListaOrdenadores((int)rdr["Id"])
                };
            }

            con.Close();
            return pedido;
        }

        public void DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var pedido = GetById(id);

                    using (var con = new SqlConnection(_connectionString))
                    {
                        var query = $"DELETE FROM Pedidos WHERE Id = {id}";
                        var cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    _loggerManager.LogDebug(
                        $"Pedido eliminado. Descripcion: {pedido.Descripcion}");
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar pedido. Message: " + ex.Message);
                throw;
            }
        }
    }
}
