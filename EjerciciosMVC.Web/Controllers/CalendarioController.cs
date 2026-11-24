using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class CalendarioController : Controller
{
    private readonly AppStore _store;

    public CalendarioController(AppStore store) => _store = store;

    public IActionResult Index(int? mes, int? anio)
    {
        var hoy = DateTime.Today;
        var m = Math.Clamp(mes ?? hoy.Month, 1, 12);
        var a = Math.Clamp(anio ?? hoy.Year, 2000, 2100);

        var primero = new DateTime(a, m, 1);
        var desplazamiento = ((int)primero.DayOfWeek + 6) % 7;
        var cursor = primero.AddDays(-desplazamiento);

        var semanas = new List<List<DateTime>>();
        for (var s = 0; s < 6; s++)
        {
            var semana = new List<DateTime>();
            for (var d = 0; d < 7; d++)
            {
                semana.Add(cursor);
                cursor = cursor.AddDays(1);
            }
            semanas.Add(semana);
        }

        var eventos = _store.Eventos
            .Where(e => e.Fecha.Month == m && e.Fecha.Year == a)
            .OrderBy(e => e.Fecha)
            .ThenBy(e => e.Hora)
            .ToList();

        var ahora = DateTime.Now;
        var recordatorios = _store.Eventos
            .Where(e => e.RecordatorioMin > 0)
            .Where(e =>
            {
                var aviso = e.Inicio.AddMinutes(-e.RecordatorioMin);
                return ahora >= aviso && ahora <= e.Inicio;
            })
            .OrderBy(e => e.Inicio)
            .ToList();

        var modelo = new CalendarioViewModel
        {
            Mes = m,
            Anio = a,
            Semanas = semanas,
            Eventos = eventos,
            Recordatorios = recordatorios
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Titulo))
        {
            TempData["Error"] = "Escribe el titulo del evento.";
            return RedirectToAction(nameof(Index), new { mes = evento.Fecha.Month, anio = evento.Fecha.Year });
        }

        if (evento.Fecha == default) evento.Fecha = DateTime.Today;
        if (string.IsNullOrWhiteSpace(evento.Hora)) evento.Hora = "09:00";

        _store.AgregarEvento(evento);
        TempData["Ok"] = "Evento agregado al calendario.";
        return RedirectToAction(nameof(Index), new { mes = evento.Fecha.Month, anio = evento.Fecha.Year });
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var evento = _store.Eventos.FirstOrDefault(e => e.Id == id);

        if (evento is null)
        {
            TempData["Error"] = "El evento no existe.";
            return RedirectToAction(nameof(Index));
        }

        return View(evento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Titulo))
        {
            TempData["Error"] = "Escribe el titulo del evento.";
            return RedirectToAction(nameof(Editar), new { id = evento.Id });
        }

        _store.ActualizarEvento(evento);
        TempData["Ok"] = "Evento actualizado.";
        return RedirectToAction(nameof(Index), new { mes = evento.Fecha.Month, anio = evento.Fecha.Year });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id, int mes, int anio)
    {
        _store.EliminarEvento(id);
        TempData["Ok"] = "Evento eliminado.";
        return RedirectToAction(nameof(Index), new { mes, anio });
    }
}
