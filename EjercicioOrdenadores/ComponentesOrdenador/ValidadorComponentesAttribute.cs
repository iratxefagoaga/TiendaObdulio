using System.ComponentModel.DataAnnotations;

namespace Ejercicio_ordenadores.ComponentesOrdenador
{
    public class ValidadorComponentesAttribute : ValidationAttribute
    {
        public override bool IsValid(object? obj)
        {
            var componente = obj as IComponente;
            if (componente is Disco)
            {
                return IsValidDisco(componente);
            }

            if (componente is Procesador)
            {

                return IsValidProcesador(componente);
            }

            if (componente is Memoria)
            {

                return IsValidMemoria(componente);
            }
            return false;
        }

        private static bool IsValidMemoria(IComponente? componente)
        {
            var memoria = componente as Memoria;
            return memoria is { Megas: > 0, Calor: > 0, Coste: > 0 };
        }

        private static bool IsValidProcesador(IComponente? componente)
        {
            var procesador = componente as Procesador;
            return procesador is { Cores: > 0, Calor: > 0, Coste: > 0 };
        }

        private static bool IsValidDisco(IComponente? componente)
        {
            var disco = componente as Disco;
            return disco is { Megas: > 0, Calor: > 0, Coste: > 0 };
        }
    }
}
