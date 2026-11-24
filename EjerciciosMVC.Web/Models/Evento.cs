namespace EjerciciosMVC.Web.Models;

public class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Hora { get; set; } = "09:00";
    public string Descripcion { get; set; } = string.Empty;
    public int RecordatorioMin { get; set; }

    public DateTime Inicio => Fecha.Date.Add(TimeSpan.Parse(string.IsNullOrWhiteSpace(Hora) ? "09:00" : Hora));
}
