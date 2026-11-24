using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class ReservasController : Controller
{
    private readonly AppStore _store;

    public ReservasController(AppStore store) => _store = store;

    public IActionResult Index(int? servicioId, DateTime? fecha)
    {
        var dia = (fecha ?? DateTime.Today).Date;
        var servicios = _store.Servicios;
        var servicio = servicios.FirstOrDefault(s => s.Id == servicioId) ?? servicios.First();

        var horas = Enumerable.Range(9, 9).Select(h => $"{h:00}:00").ToList();
        var ocupadas = _store.Reservas
            .Where(r => r.Fecha.Date == dia)
            .Select(r => r.Hora)
            .ToHashSet();

        var modelo = new ReservasViewModel
        {
            Servicios = servicios,
            ServicioSeleccionado = servicio,
            Fecha = dia,
            Horas = horas,
            Ocupadas = ocupadas,
            Reservas = _store.Reservas
                .Where(r => r.Fecha.Date == dia)
                .OrderBy(r => r.Hora)
                .ToList()
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Reservar(int servicioId, DateTime fecha, string hora, string cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente))
        {
            TempData["Error"] = "Escribe el nombre de la persona que reserva.";
        }
        else if (string.IsNullOrWhiteSpace(hora))
        {
            TempData["Error"] = "Selecciona un horario disponible.";
        }
        else if (_store.Servicios.All(s => s.Id != servicioId))
        {
            TempData["Error"] = "El servicio seleccionado no existe.";
        }
        else if (!_store.AgregarReserva(servicioId, fecha, hora, cliente))
        {
            TempData["Error"] = "Ese horario ya esta ocupado. Elige otro.";
        }
        else
        {
            TempData["Ok"] = $"Reserva creada para {cliente.Trim()} a las {hora}.";
        }

        return RedirectToAction(nameof(Index), new { servicioId, fecha = fecha.ToString("yyyy-MM-dd") });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirmar(int id, int servicioId, DateTime fecha)
    {
        _store.ConfirmarReserva(id);
        TempData["Ok"] = "Reserva confirmada.";
        return RedirectToAction(nameof(Index), new { servicioId, fecha = fecha.ToString("yyyy-MM-dd") });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cancelar(int id, int servicioId, DateTime fecha)
    {
        _store.CancelarReserva(id);
        TempData["Ok"] = "Reserva cancelada.";
        return RedirectToAction(nameof(Index), new { servicioId, fecha = fecha.ToString("yyyy-MM-dd") });
    }
}
