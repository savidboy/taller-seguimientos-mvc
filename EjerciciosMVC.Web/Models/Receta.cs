namespace EjerciciosMVC.Web.Models;

public class Receta
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoComida { get; set; } = "Almuerzo";
    public string Ingredientes { get; set; } = string.Empty;
    public string Pasos { get; set; } = string.Empty;
    public string Autor { get; set; } = "Chef";
    public bool Guardada { get; set; }
    public bool Compartida { get; set; }
    public DateTime CreadaEn { get; set; } = DateTime.Now;
}
