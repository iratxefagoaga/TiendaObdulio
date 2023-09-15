using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;

namespace MVC_ComponentesCodeFirst.Models
{
    public class FactoriaContextos : IDesignTimeDbContextFactory<OrdenadoresContext>
    {
        public OrdenadoresContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<OrdenadoresContext>();
            

            var path = Directory.GetCurrentDirectory();

            var connectionString =
                "Server=(localdb)\\MSSQLLocalDB;AttachDBFilename=[DataDirectory]\\App_Data\\TiendaOrdenadoresDatabase.mdf;Trusted_Connection=True;MultipleActiveResultSets=true".Replace("[DataDirectory]", path); 
            dbContextBuilder.UseSqlServer(connectionString);
            return new OrdenadoresContext(dbContextBuilder.Options);
        }
    }
}