using System.ComponentModel.DataAnnotations;
using Ejercicio_ordenadores.ComponentesOrdenador;

namespace Ejercicio_ordenadores.Builders.Componentes
{
    public class ComponenteBuilder : IComponenteBuilder
    {
        public IComponente GetDisco(Discos modeloDisco)
        {
            var disco = CrearDisco(modeloDisco);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            if (validador.IsValid(disco))
                return disco;
            return new Disco();
        }
        private static IComponente CrearDisco(Discos modeloDisco)
        {
            IComponente disco = new Disco();
            return modeloDisco switch
            {
                Discos.Disco789Xx => new Disco("DiscoDuro SanDisk",500000, 50, 10, "789-XX"),
                Discos.Disco789Xx2 => new Disco("DiscoDuro SanDisk", 1000000, 90, 29, "789-XX2"),
                Discos.Disco789Xx3 => new Disco("DiscoDuro SanDisk", 2000000, 128, 39, "789-XX3"),
                Discos.Disco788Fg => new Disco("Disco Mecánico Patatin",250, 37, 35, "788-FG"),
                Discos.Disco788Fg2 => new Disco("Disco Mecánico Patatin", 250, 67, 35, "788-FG2"),
                Discos.Disco788Fg3 => new Disco("Disco Mecánico Patatin", 250, 97, 35, "788-FG3"),
                Discos.Disco1789Xcs => new Disco("Disco Externo Sam",9000000, 134, 10,
                    "1789XCS"),
                Discos.Disco1789Xcd => new Disco("Disco Externo Sam", 10000000, 138, 12,
                    "1789XCD"),
                Discos.Disco1789Xct => new Disco("Disco Externo Sam", 11000000, 140, 22,
                    "1789XCT"),
                _ => disco
            };
        }

        public IComponente GetMemoria(Memorias modeloMemoria)
        {
            var memoria = CrearMemoria(modeloMemoria);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            if (validador.IsValid(memoria))
                return memoria;
            return new Memoria();
        }
        private static IComponente CrearMemoria(Memorias modeloMemoria)
        {
            IComponente memoria = new Memoria();
            return modeloMemoria switch
            {
                Memorias.Memoria879Fh => new Memoria("Banco de Memoria SDRAM",512000, 100, 10,
                    "879-FH"),
                Memorias.Memoria879Fhl => new Memoria("Banco de Memoria SDRAM", 1000, 125, 15,
                    "879-FHL"),
                Memorias.Memoria879Fht => new Memoria("Banco de Memoria SDRAM", 2000, 150, 24,
                    "879-FHT"),
                _ => memoria
            };
        }

        public IComponente GetProcesador(Procesadores modeloProcesador)
        {
            var procesador = CrearProcesador(modeloProcesador);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            if (validador.IsValid(procesador))
                return procesador;
            return new Procesador();
        }
        private static IComponente CrearProcesador(Procesadores modeloProcesador)
        {
            IComponente procesador = new Procesador();
            return modeloProcesador switch
            {
                Procesadores.Procesador789Xcs => new Procesador("Procesador Intel i7",9, 134, 10, "789-XCS"),
                Procesadores.Procesador789Xcd => new Procesador("Procesador Intel i7",10, 138, 12, "789 -XCD"),
                Procesadores.Procesador789Xct => new Procesador("Procesador Intel i7", 11, 138, 22, "789-XCT"),
                Procesadores.Procesador797X => new Procesador("Procesador Ryzen AMD", 10, 78, 30, "797-X"),
                Procesadores.Procesador797X2 => new Procesador("Procesador Ryzen AMD", 29, 178, 30, "797-X2"),
                Procesadores.Procesador797X3 => new Procesador("Procesador Ryzen AMD", 34, 278, 60, "797-X3"),
                _ => procesador
            };
        }
    }
}
