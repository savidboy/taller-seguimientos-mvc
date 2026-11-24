namespace EjerciciosMVC.Web.Models;

public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool Completada { get; set; }
    public DateTime CreadaEn { get; set; } = DateTime.Now;
}
