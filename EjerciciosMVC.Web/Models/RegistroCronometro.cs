namespace EjerciciosMVC.Web.Models;

public class RegistroCronometro
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tiempo { get; set; } = "00:00.00";
    public int Vueltas { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}
