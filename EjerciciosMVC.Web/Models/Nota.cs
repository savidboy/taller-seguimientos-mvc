namespace EjerciciosMVC.Web.Models;

public class Nota
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public string Categoria { get; set; } = "General";
    public DateTime ActualizadaEn { get; set; } = DateTime.Now;
}
