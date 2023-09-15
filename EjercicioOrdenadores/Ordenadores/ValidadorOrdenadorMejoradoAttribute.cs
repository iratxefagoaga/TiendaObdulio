using System.ComponentModel.DataAnnotations;
using Ejercicio_ordenadores.ComponentesOrdenador;

namespace Ejercicio_ordenadores.Ordenadores
{
    public class ValidadorOrdenadorMejoradoAttribute : ValidationAttribute
    {
        public override bool IsValid (Object? value)
        {
            if (value is not OrdenadorMejorado ordenadorMejorado)
                return false;
            if (ordenadorMejorado.DiscosSecundarios is { Count: >= 1 })
            {
                if (ordenadorMejorado.DiscosSecundarios.Any(discoSecundario => discoSecundario is not Disco))
                {
                    return false;
                }
            }

            if (ordenadorMejorado.Procesador == null || ordenadorMejorado.Disco == null ||
                ordenadorMejorado.Memoria == null)
                return false;
            return ordenadorMejorado is { Procesador: Procesador, Disco: Disco, Memoria: Memoria };
        }
    }
}
