using System.Globalization;

namespace EjerciciosMVC.Web.Models;

public class PropinaViewModel
{
    public decimal Monto { get; set; }
    public decimal Porcentaje { get; set; } = 10;
    public int Personas { get; set; } = 1;
    public decimal Propina { get; set; }
    public decimal Total { get; set; }
    public decimal PorPersona { get; set; }
    public bool Calculado { get; set; }
}

public class ContrasenaViewModel
{
    public int Longitud { get; set; } = 16;
    public bool Mayusculas { get; set; } = true;
    public bool Minusculas { get; set; } = true;
    public bool Numeros { get; set; } = true;
    public bool Simbolos { get; set; } = true;
    public string? Resultado { get; set; }
    public double Entropia { get; set; }
    public string? Nivel { get; set; }
}

public class CalendarioViewModel
{
    public int Mes { get; set; }
    public int Anio { get; set; }
    public List<List<DateTime>> Semanas { get; set; } = new();
    public List<Evento> Eventos { get; set; } = new();
    public List<Evento> Recordatorios { get; set; } = new();

    public string NombreMes => new DateTime(Anio, Mes, 1)
        .ToString("MMMM 'de' yyyy", new CultureInfo("es-CO"));
}

public class MemoriaViewModel
{
    public string Dificultad { get; set; } = "facil";
    public string EmojisJson { get; set; } = "[]";
    public List<PuntajeMemoria> Puntajes { get; set; } = new();
}

public class ReservasViewModel
{
    public List<Servicio> Servicios { get; set; } = new();
    public Servicio? ServicioSeleccionado { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public List<string> Horas { get; set; } = new();
    public HashSet<string> Ocupadas { get; set; } = new();
    public List<Reserva> Reservas { get; set; } = new();
}

public class GastosViewModel
{
    public List<Gasto> Gastos { get; set; } = new();
    public List<string> Categorias { get; set; } = new();
    public string? CategoriaFiltro { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }
    public decimal TotalMes { get; set; }
    public Dictionary<string, decimal> PorCategoria { get; set; } = new();
}

public class RecetasViewModel
{
    public List<Receta> Recetas { get; set; } = new();
    public string? Q { get; set; }
    public string? Tipo { get; set; }
    public bool SoloGuardadas { get; set; }
    public List<string> Tipos { get; set; } = new();
}
