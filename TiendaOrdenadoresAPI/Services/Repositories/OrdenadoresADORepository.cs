using System.Data;
using Microsoft.Data.SqlClient;
using TiendaOrdenadoresAPI.Logging;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.Repositories
{
    public class OrdenadoresAdoRepository : IOrdenadorRepository
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly string? _connectionString = Configuration.GetConnectionString("OrdenadoresContext")
            ?.Replace("[DataDirectory]", Directory.GetCurrentDirectory());

        private readonly ILoggerManager _loggerManager;

        public OrdenadoresAdoRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void Add(Ordenador ordenador)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    "INSERT INTO Ordenadores (Descripcion, PedidoId) VALUES (@Descripcion, @PedidoId)";
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = ordenador.Descripcion;
                cmd.Parameters.Add("@PedidoId", SqlDbType.Int).Value = ordenador.PedidoId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public List<Ordenador> All()
        {
            try
            {
                List<Ordenador> listaOrdenadores = new();
                using var con = new SqlConnection(_connectionString);
                var query =
                    "SELECT Ordenadores.Id, Ordenadores.Descripcion, Ordenadores.PedidoId, " +
                    "Pedidos.Id AS IdPedido, Pedidos.Descripcion AS DescripcionPedido, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId " +
                    "FROM Ordenadores " +
                    "INNER JOIN Pedidos ON Ordenadores.PedidoId = Pedidos.Id";
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
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al obtener lista de ordenadors. Message: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var ordenador = GetById(id);
                using (var con = new SqlConnection(_connectionString))
                {
                    var query = $"DELETE FROM Ordenadores WHERE Id = {id}";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                _loggerManager.LogDebug(
                    $"Ordenador eliminado. Descripcion: {ordenador.Descripcion}");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public void Edit(Ordenador ordenador)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    $"UPDATE Ordenadores SET Descripcion = @Descripcion, PedidoId = @PedidoId WHERE Id = {ordenador.Id} ";
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = ordenador.Descripcion;
                cmd.Parameters.Add("@PedidoId", SqlDbType.Int).Value = ordenador.PedidoId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public Ordenador GetById(int id)
        {
            Ordenador ordenador = new();
            using var con = new SqlConnection(_connectionString);
            var sqlQuery =
                "SELECT Ordenadores.Descripcion, Ordenadores.Id, Ordenadores.PedidoId, " +
                "Pedidos.Id AS IdPedido, Pedidos.Descripcion AS DescripcionPedido, Pedidos.Fecha, Pedidos.FacturaId, Pedidos.ClienteId " +
                "FROM Ordenadores INNER JOIN Pedidos ON Ordenadores.PedidoId = Pedidos.Id WHERE Ordenadores.Id= " +
                id;
            var cmd = new SqlCommand(sqlQuery, con);
            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ordenador = new Ordenador
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
            }

            con.Close();
            return ordenador;
        }

        public void DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var ordenador = GetById(id);

                    using (var con = new SqlConnection(_connectionString))
                    {
                        var query = $"DELETE FROM Ordenadores WHERE Id = {id}";
                        var cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Descripcion: {ordenador.Descripcion}");
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        private List<Componente> ListaComponentes(int ordenadorId)
        {
            List<Componente> listaComponentes = new();
            using var con = new SqlConnection(_connectionString);
            var query =
                "SELECT Id, TipoComponente, Serie, Descripcion, Calor, Megas, Cores, Precio, OrdenadorId " +
                "FROM Componentes WHERE OrdenadorId = "+ordenadorId;
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
    }
}
