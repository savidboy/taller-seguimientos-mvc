using EjerciciosMVC.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class TareasController : Controller
{
    private readonly AppStore _store;

    public TareasController(AppStore store) => _store = store;

    public IActionResult Index()
    {
        var tareas = _store.Tareas
            .OrderBy(t => t.Completada)
            .ThenByDescending(t => t.CreadaEn)
            .ToList();

        return View(tareas);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Agregar(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            TempData["Error"] = "Escribe el titulo de la tarea.";
        }
        else
        {
            _store.AgregarTarea(titulo);
            TempData["Ok"] = "Tarea agregada.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Alternar(int id)
    {
        _store.AlternarTarea(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        _store.EliminarTarea(id);
        TempData["Ok"] = "Tarea eliminada.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LimpiarCompletadas()
    {
        _store.LimpiarCompletadas();
        TempData["Ok"] = "Se quitaron las tareas completadas.";
        return RedirectToAction(nameof(Index));
    }
}
