namespace EjerciciosMVC.Web.Models;

public class Servicio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int DuracionMin { get; set; }
}

public class Reserva
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Hora { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public bool Confirmada { get; set; }
    public DateTime CreadaEn { get; set; } = DateTime.Now;
}
