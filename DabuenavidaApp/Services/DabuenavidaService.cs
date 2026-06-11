using DabuenavidaApp.Models;

namespace DabuenavidaApp.Services
{
    public class DabuenavidaService
    {
        private readonly List<DabuenavidaModel> _datos = GenerarDatosMock();

        private static List<DabuenavidaModel> GenerarDatosMock()
        {
            var centros = new[]
            {
                "101 - Agencia Central",
                "102 - Agencia Kennedy",
                "103 - Agencia Miraflores",
                "178 - Agencia San Pedro Sula",
                "180 - Agencia Norte"
            };

            var nombres = new[]
            {
                "Roberto Alfaro Peña", "Lucia Hernández Vega", "Marco Antonio Salgado",
                "Patricia Fuentes López", "Jorge Reyes Molina", "Sandra Mejía Contreras",
                "Diego Castellanos Ríos", "Ana María Reconco", "Carlos Funez Paz",
                "Laura Discua Flores", "José Pineda Torres", "María López Soto"
            };

            var random = new Random(42);
            var lista = new List<DabuenavidaModel>();

            for (int i = 0; i < 60; i++)
            {
                var fechaCreacion = new DateTime(2025, 1, 1).AddDays(i);
                lista.Add(new DabuenavidaModel
                {
                    NumeroDabuenavida = (5013415625 + i).ToString(),
                    NombreCliente = nombres[i % nombres.Length],
                    DNI = $"{random.Next(100, 999):D4}-{random.Next(1970, 2005)}-{random.Next(10000, 99999)}",
                    CentroDeCosto = centros[i % centros.Length],
                    FechaCreacion = fechaCreacion,
                    FechaVencimiento = fechaCreacion.AddYears(5),
                    ValorDAB = random.Next(5, 201) * 1000,
                    NumeroTitulos = random.Next(5, 45),
                    EstadoImpresion = i % 2 == 0 ? "NO IMPRESO" : "IMPRESO"
                });
            }

            return lista;
        }

        public PagedResult ObtenerDatos(int pagina, int tamañoPagina, FiltrosBusqueda filtros)
        {
            var query = _datos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.TextoBusqueda))
            {
                var busqueda = filtros.TextoBusqueda.Trim().ToLower();
                query = query.Where(x =>
                    x.NumeroDabuenavida.ToLower().Contains(busqueda) ||
                    x.DNI.ToLower().Contains(busqueda)
                );
            }

            if (!string.IsNullOrWhiteSpace(filtros.CentroDeCosto))
            {
                query = query.Where(x => x.CentroDeCosto == filtros.CentroDeCosto);
            }

            var total = query.Count();
            var data = query
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            return new PagedResult
            {
                Data = data,
                TotalRegistros = total,
                Pagina = pagina,
                TamañoPagina = tamañoPagina
            };
        }

        public List<string> ObtenerCentros()
        {
            return _datos
                .Select(x => x.CentroDeCosto)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }
    }
}
