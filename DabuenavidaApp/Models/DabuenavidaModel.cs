namespace DabuenavidaApp.Models
{
    public class DabuenavidaModel
    {
        public string NumeroDabuenavida { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public string DNI { get; set; } = "";
        public string CentroDeCosto { get; set; } = "";
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal ValorDAB { get; set; }
        public int NumeroTitulos { get; set; }
        public string EstadoImpresion { get; set; } = "";
    }

    public class PagedResult
    {
        public List<DabuenavidaModel> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamañoPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamañoPagina);
    }

    public class FiltrosBusqueda
    {
        public string TextoBusqueda { get; set; } = "";
        public string CentroDeCosto { get; set; } = "";
    }
}
