
using Ejercicio_ordenadores.Models;

namespace Ejercicio_ordenadores
{
    internal class Program
    {
        protected Program()
        {

        }
        static void Main()
        {
            Console.WriteLine("Ordenador se añade a database");
            using var db = new AppDbContext();
            Componente componente = new()
            {
                Calor = 10,
                Cores = 10,
                Descripcion = "Añado un nuevo componente",
                Megas = 0,
                Precio = 200,
                Serie = "NUMERO SERIE",
                TipoComponente = (int)EnumTipoComponentes.Procesador
            };
            Models.Ordenador ordenador = new()
            {
                Descripcion = "Descripcion",
                Componentes = new List<Componente> { componente }
            };

            db.Ordenadores?.Add(ordenador);
            db.SaveChanges();

            // Display all Ordenadores from the database
            var query = from b in db.Ordenadores
                orderby b.Descripcion
                select b;

            Console.WriteLine("All ordenadores in the database:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Descripcion);
            }
            // Display all Componentes from the database
            var query1 = from b in db.Componentes
                orderby b.Serie
                select b;

            Console.WriteLine("All componentes in the database:");
            foreach (var item in query1)
            {
                Console.WriteLine(item.Descripcion);
            }
        }
    }
}