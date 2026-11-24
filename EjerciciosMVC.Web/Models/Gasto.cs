namespace EjerciciosMVC.Web.Models;

public class Gasto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = "Otros";
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
}
