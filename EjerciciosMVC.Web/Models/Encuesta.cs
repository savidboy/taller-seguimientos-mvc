namespace EjerciciosMVC.Web.Models;

public class Opcion
{
    public int Id { get; set; }
    public string Texto { get; set; } = string.Empty;
    public int Votos { get; set; }
}

public class Encuesta
{
    public int Id { get; set; }
    public string Pregunta { get; set; } = string.Empty;
    public List<Opcion> Opciones { get; set; } = new();
    public DateTime CreadaEn { get; set; } = DateTime.Now;

    public int TotalVotos => Opciones.Sum(o => o.Votos);
}
