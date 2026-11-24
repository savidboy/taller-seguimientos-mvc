namespace EjerciciosMVC.Web.Models;

public class PuntajeMemoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Movimientos { get; set; }
    public int Segundos { get; set; }
    public string Dificultad { get; set; } = "facil";
    public DateTime Fecha { get; set; } = DateTime.Now;
}
