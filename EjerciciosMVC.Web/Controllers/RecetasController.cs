using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class RecetasController : Controller
{
    private readonly AppStore _store;

    public RecetasController(AppStore store) => _store = store;

    public IActionResult Index(string? q, string? tipo, bool soloGuardadas = false)
    {
        var consulta = _store.Recetas.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            consulta = consulta.Where(r =>
                r.Nombre.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                r.Ingredientes.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            consulta = consulta.Where(r => r.TipoComida == tipo);
        }

        if (soloGuardadas)
        {
            consulta = consulta.Where(r => r.Guardada);
        }

        var modelo = new RecetasViewModel
        {
            Recetas = consulta.OrderBy(r => r.Nombre).ToList(),
            Q = q,
            Tipo = tipo,
            SoloGuardadas = soloGuardadas,
            Tipos = _store.Recetas.Select(r => r.TipoComida).Distinct().OrderBy(t => t).ToList()
        };

        return View(modelo);
    }

    public IActionResult Detalle(int id)
    {
        var receta = _store.Recetas.FirstOrDefault(r => r.Id == id);

        if (receta is null)
        {
            TempData["Error"] = "La receta no existe.";
            return RedirectToAction(nameof(Index));
        }

        return View(receta);
    }

    [HttpGet]
    public IActionResult Crear() => View(new Receta());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Receta receta)
    {
        if (string.IsNullOrWhiteSpace(receta.Nombre) || string.IsNullOrWhiteSpace(receta.Ingredientes))
        {
            TempData["Error"] = "El nombre y los ingredientes son obligatorios.";
            return View(receta);
        }

        receta.Nombre = receta.Nombre.Trim();
        receta.Autor = string.IsNullOrWhiteSpace(receta.Autor) ? "Yo" : receta.Autor.Trim();
        _store.AgregarReceta(receta);

        TempData["Ok"] = "Receta publicada.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Guardar(int id)
    {
        _store.AlternarGuardada(id);
        TempData["Ok"] = "Se actualizo tu lista de recetas guardadas.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Compartir(int id)
    {
        _store.MarcarCompartida(id);
        var enlace = Url.Action(nameof(Detalle), "Recetas", new { id }, Request.Scheme);
        TempData["Ok"] = $"Receta compartida. Enlace: {enlace}";
        return RedirectToAction(nameof(Detalle), new { id });
    }
}
