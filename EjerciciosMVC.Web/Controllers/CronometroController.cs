using EjerciciosMVC.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class CronometroController : Controller
{
    private readonly AppStore _store;

    public CronometroController(AppStore store) => _store = store;

    public IActionResult Index() =>
        View(_store.Registros.OrderByDescending(r => r.Fecha).Take(20).ToList());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Guardar(string nombre, string tiempo, int vueltas)
    {
        if (string.IsNullOrWhiteSpace(tiempo))
        {
            TempData["Error"] = "El cronometro no tiene un tiempo para guardar.";
            return RedirectToAction(nameof(Index));
        }

        _store.AgregarRegistro(nombre, tiempo.Trim(), Math.Clamp(vueltas, 0, 999));
        TempData["Ok"] = "Sesion guardada.";
        return RedirectToAction(nameof(Index));
    }
}
