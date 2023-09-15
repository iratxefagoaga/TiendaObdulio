using System.Data;
using Microsoft.Data.SqlClient;
using TiendaOrdenadoresAPI.Logging;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.Repositories
{
    public class ClienteAdoRepository : IClienteRepository
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly string? _connectionString = Configuration.GetConnectionString("OrdenadoresContext")?.Replace("[DataDirectory]",Directory.GetCurrentDirectory());
        private readonly ILoggerManager _loggerManager;
        public ClienteAdoRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void Add(Cliente cliente)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    "INSERT INTO Clientes (Nombre, Apellido, Email, Password, CreditCard) " +
                    "VALUES (@Nombre, @Apellido, @Email, @Password, @CreditCard)";
                using var cmd = new SqlCommand(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar,50).Value = cliente.Nombre;
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = cliente.Apellido;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.Password;
                cmd.Parameters.Add("@CreditCard", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.CreditCard;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar cliente. Message: "+ex.Message);
                throw;
            }
        }

        public List<Cliente> All()
        {
            try
            {
                List<Cliente> listaClientes = new();
                using var con = new SqlConnection(_connectionString);
                var query = "SELECT Id, Nombre, Apellido, Email, Password, CreditCard\r\nFROM     Clientes";
                var cmd = new SqlCommand(query, con);
                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var cliente = new Cliente
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Nombre = rdr["Nombre"].ToString(),
                        Apellido = rdr["Apellido"].ToString(),
                        CreditCard = rdr["CreditCard"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Password = rdr["Password"].ToString(),
                        Pedidos = ListaPedidos((int)rdr["Id"])
                    };
                    listaClientes.Add(cliente);
                }

                con.Close();

                return listaClientes;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al obtener lista de clientes. Message: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var cliente = GetById(id);
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = $"DELETE FROM Clientes WHERE Id = {id}";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                _loggerManager.LogDebug(
                    $"Cliente eliminado. Nombre: {cliente.Nombre}");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar cliente. Message: " + ex.Message);
                throw;
            }
        }

        public void Edit(Cliente cliente)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    "UPDATE Clientes " +
                    "SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Password = @Password, CreditCard = @CreditCard WHERE Id = "+ cliente.Id;
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = cliente.Nombre;
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = cliente.Apellido;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.Password;
                cmd.Parameters.Add("@CreditCard", SqlDbType.VarChar, Int32.MaxValue).Value = cliente.CreditCard;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar cliente. Message: " + ex.Message);
                throw;
            }
        }

        public Cliente GetById(int? id)
        {
            try
            {
                var cliente = new Cliente();

                using var con = new SqlConnection(_connectionString);
                var sqlQuery =
                    "SELECT Id, Nombre, Apellido, Email, Password, CreditCard\r\nFROM     Clientes WHERE Id= " + id;
                var cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cliente.Id = Convert.ToInt32(rdr["Id"]);
                    cliente.Nombre = rdr["Nombre"].ToString();
                    cliente.Apellido = rdr["Apellido"].ToString();
                    cliente.CreditCard = rdr["CreditCard"].ToString();
                    cliente.Email = rdr["Email"].ToString();
                    cliente.Password = rdr["Password"].ToString();
                    cliente.Pedidos = ListaPedidos((int)rdr["Id"]);
                }

                con.Close();
                return cliente;
            }

            catch(Exception ex)
            {
                _loggerManager.LogError("Error al obtener cliente. Message: " + ex.Message);
                throw;
            }
        }

        private List<Pedido> ListaPedidos(int clienteId)
        {
            List<Pedido> listaPedidos = new();
            using var con = new SqlConnection(_connectionString);
            var query =
                "SELECT Pedidos.Id, Pedidos.Descripcion, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId, " +
                "Clientes.Id AS IdCliente, Clientes.Nombre, Clientes.Apellido, Clientes.Email, Clientes.Password, Clientes.CreditCard, " +
                "Facturas.Id AS IdFactura, Facturas.Descripcion AS DescripcionFactura, Facturas.Fecha AS FechaFactura " +
                "FROM     Pedidos " +
                "INNER JOIN Facturas ON Pedidos.FacturaId = Facturas.Id " +
                "INNER JOIN Clientes ON Pedidos.ClienteId = Clientes.Id " +
                "WHERE Pedidos.ClienteId = "+clienteId;
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
                listaPedidos.Add(pedido);
            }

            con.Close();

            return listaPedidos;
        }
        private List<Ordenador> ListaOrdenadores(int pedidoId)
        {
            List<Ordenador> listaOrdenadores = new();
            using var con = new SqlConnection(_connectionString);
            var query =
                "SELECT Ordenadores.Id, Ordenadores.Descripcion, Ordenadores.PedidoId, " +
                "Pedidos.Id AS IdPedido, Pedidos.Descripcion AS DescripcionPedido, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId " +
                "FROM Ordenadores " +
                "INNER JOIN Pedidos ON Ordenadores.PedidoId = Pedidos.Id " +
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
        public void DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var cliente = GetById(id);

                    using (var con = new SqlConnection(_connectionString))
                    {
                        var query = $"DELETE FROM Clientes WHERE Clientes.ID = {id}";
                        var cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Nombre: {cliente.Nombre}");
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar cliente. Message: " + ex.Message);
                throw;
            }
        }
    }
}
