using EjerciciosMVC.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class EncuestasController : Controller
{
    private readonly AppStore _store;

    public EncuestasController(AppStore store) => _store = store;

    public IActionResult Index() =>
        View(_store.Encuestas.OrderByDescending(e => e.CreadaEn).ToList());

    [HttpGet]
    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(string pregunta, string opciones)
    {
        var lista = (opciones ?? string.Empty)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        if (string.IsNullOrWhiteSpace(pregunta))
        {
            TempData["Error"] = "Escribe la pregunta de la encuesta.";
            return RedirectToAction(nameof(Crear));
        }

        if (lista.Count < 2)
        {
            TempData["Error"] = "Agrega al menos dos opciones (una por linea).";
            return RedirectToAction(nameof(Crear));
        }

        _store.AgregarEncuesta(pregunta, lista);
        TempData["Ok"] = "Encuesta creada.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Votar(int encuestaId, int opcionId)
    {
        _store.Votar(encuestaId, opcionId);
        TempData["Ok"] = "Voto registrado.";
        return RedirectToAction(nameof(Resultados), new { id = encuestaId });
    }

    public IActionResult Resultados(int id)
    {
        var encuesta = _store.Encuestas.FirstOrDefault(e => e.Id == id);

        if (encuesta is null)
        {
            TempData["Error"] = "La encuesta no existe.";
            return RedirectToAction(nameof(Index));
        }

        return View(encuesta);
    }
}
