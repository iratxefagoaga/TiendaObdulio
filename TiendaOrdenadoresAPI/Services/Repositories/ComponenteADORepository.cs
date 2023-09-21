using Microsoft.Data.SqlClient;
using System.Data;
using TiendaOrdenadoresAPI.Logging;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;
using Componente = TiendaOrdenadoresAPI.Models.Componente;

namespace TiendaOrdenadoresAPI.Services.Repositories
{
    public class ComponenteAdoRepository : IGenericRepositoryAdo<Componente>
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly string? _connectionString = Configuration.GetConnectionString("OrdenadoresContext")
            ?.Replace("[DataDirectory]", Directory.GetCurrentDirectory());

        private readonly ILoggerManager _loggerManager;

        public ComponenteAdoRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void Add(Componente componente)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    "INSERT INTO Componentes (TipoComponente, Descripcion, Serie, Calor, Megas, Cores, Precio, OrdenadorId) " +
                    "VALUES (@TipoComponente, @Descripcion, @Serie, @Calor, @Megas, @Cores, @Precio, @OrdenadorId)";
                //var cmd = new SqlCommand(query, con);
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@TipoComponente", SqlDbType.Int).Value = componente.TipoComponente;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar,100).Value = componente.Descripcion;
                cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 100).Value = componente.Serie;
                cmd.Parameters.Add("@Calor", SqlDbType.Int).Value = componente.Calor;
                cmd.Parameters.Add("@Megas", SqlDbType.BigInt).Value = componente.Megas;
                cmd.Parameters.Add("@Cores", SqlDbType.Int).Value = componente.Cores;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = componente.Precio;
                cmd.Parameters.Add("@OrdenadorId", SqlDbType.Int).Value = componente.OrdenadorId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar componente. Message: " + ex.Message);
                throw;
            }
        }

        public List<Componente> All()
        {
            try
            {
                List<Componente> listaComponentes = new();
                using var con = new SqlConnection(_connectionString);
                var query =
                    "SELECT Componentes.Id, Componentes.TipoComponente, Componentes.Descripcion, Componentes.Serie, Componentes.Calor, Componentes.Megas, Componentes.Cores, Componentes.Precio, Componentes.OrdenadorId, " +
                    "Ordenadores.Id AS IdOrdenador, Ordenadores.Descripcion AS DescripcionOrdenador, Ordenadores.PedidoId " +
                    "FROM Componentes " +
                    "INNER JOIN Ordenadores ON Componentes.OrdenadorId = Ordenadores.Id";
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
                        Megas = Convert.ToInt64( rdr["Megas"]),
                        OrdenadorId = Convert.ToInt32(rdr["OrdenadorId"]),
                        Precio = (decimal)rdr["Precio"],
                        Serie = rdr["Serie"].ToString(),
                        TipoComponente =
                            (Ejercicio_ordenadores.Builders.Componentes.EnumTipoComponentes)rdr["TipoComponente"],
                        Ordenador = new Ordenador()
                        {
                            Id = (int)rdr["IdOrdenador"],
                            Descripcion = rdr["DescripcionOrdenador"].ToString()
                        }
                    };
                    listaComponentes.Add(componente);
                }

                con.Close();

                return listaComponentes;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al obtener lista de componentes. Message: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var componente = GetById(id);
                    using (var con = new SqlConnection(_connectionString))
                    {
                        var query = $"DELETE FROM Componentes WHERE Id = {id}";
                        var cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    _loggerManager.LogDebug(
                        $"Componente eliminado. Serie: {componente.Serie}");

                
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar componente. Message: " + ex.Message);
                throw;
            }
        }

        public void Edit(Componente componente)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                var query =
                    "UPDATE Componentes " +
                    "SET TipoComponente = @TipoComponente, Descripcion = @Descripcion, Serie = @Serie, Calor = @Calor, Megas = @Megas, Cores = @Cores, Precio = @Precio, OrdenadorId = @OrdenadorId WHERE Id = "+ componente.Id;
                using SqlCommand cmd = new(query, con);
                // define parameters and their values
                cmd.Parameters.Add("@TipoComponente", SqlDbType.Int).Value = componente.TipoComponente;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = componente.Descripcion;
                cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 100).Value = componente.Serie;
                cmd.Parameters.Add("@Calor", SqlDbType.Int).Value = componente.Calor;
                cmd.Parameters.Add("@Megas", SqlDbType.BigInt).Value = componente.Megas;
                cmd.Parameters.Add("@Cores", SqlDbType.Int).Value = componente.Cores;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = componente.Precio;
                cmd.Parameters.Add("@OrdenadorId", SqlDbType.Int).Value = componente.OrdenadorId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar componente. Message: " + ex.Message);
                throw;
            }
        }

        public Componente? GetById(int id)
        {
            Componente componente = new();
            using var con = new SqlConnection(_connectionString);
            var sqlQuery =
                "SELECT Componentes.Id, Componentes.TipoComponente, Componentes.Descripcion, Componentes.Serie, Componentes.Calor, Componentes.Megas, Componentes.Cores, Componentes.Precio, Componentes.OrdenadorId, " +
                "Ordenadores.Id AS IdOrdenador, Ordenadores.Descripcion AS DescripcionOrdenador, Ordenadores.PedidoId " +
                "FROM Componentes " +
                "INNER JOIN Ordenadores ON Componentes.OrdenadorId = Ordenadores.Id " +
                "WHERE Componentes.Id = " + id;
            var cmd = new SqlCommand(sqlQuery, con);
            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                componente = new Componente
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Calor = Convert.ToInt32(rdr["Calor"]),
                    Cores = Convert.ToInt32(rdr["Cores"]),
                    Descripcion = rdr["Descripcion"].ToString(),
                    Megas = (long)rdr["Megas"],
                    OrdenadorId = Convert.ToInt32(rdr["OrdenadorId"]),
                    Precio = (decimal)rdr["Precio"],
                    Serie = rdr["Serie"].ToString(),
                    TipoComponente =
                        (Ejercicio_ordenadores.Builders.Componentes.EnumTipoComponentes)rdr["TipoComponente"],
                    Ordenador = new Ordenador()
                    {
                    Id = (int)rdr["IdOrdenador"],
                    Descripcion = rdr["DescripcionOrdenador"].ToString()
                }
                };
            }

            return componente;
        }

        public void DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var componente = GetById(id);

                    using (var con = new SqlConnection(_connectionString))
                    {
                        var query = $"DELETE FROM Componentes WHERE Id = {id}";
                        var cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Serie: {componente.Serie}");
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar componente. Message: " + ex.Message);
                throw;
            }
        }
    }
}
