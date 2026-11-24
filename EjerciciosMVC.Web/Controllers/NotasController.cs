using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class NotasController : Controller
{
    private readonly AppStore _store;

    public NotasController(AppStore store) => _store = store;

    public IActionResult Index(string? q, string? categoria)
    {
        var consulta = _store.Notas.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            consulta = consulta.Where(n =>
                n.Titulo.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                n.Contenido.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            consulta = consulta.Where(n => n.Categoria == categoria);
        }

        ViewBag.Categorias = _store.Notas
            .Select(n => n.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        ViewBag.Q = q;
        ViewBag.Categoria = categoria;

        return View(consulta.OrderByDescending(n => n.ActualizadaEn).ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(string titulo, string contenido, string categoria)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(contenido))
        {
            TempData["Error"] = "El titulo y el contenido son obligatorios.";
            return RedirectToAction(nameof(Index));
        }

        _store.AgregarNota(new Nota
        {
            Titulo = titulo.Trim(),
            Contenido = contenido.Trim(),
            Categoria = string.IsNullOrWhiteSpace(categoria) ? "General" : categoria.Trim()
        });

        TempData["Ok"] = "Nota guardada.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var nota = _store.Notas.FirstOrDefault(n => n.Id == id);

        if (nota is null)
        {
            TempData["Error"] = "La nota no existe.";
            return RedirectToAction(nameof(Index));
        }

        return View(nota);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Nota nota)
    {
        if (string.IsNullOrWhiteSpace(nota.Titulo) || string.IsNullOrWhiteSpace(nota.Contenido))
        {
            TempData["Error"] = "El titulo y el contenido son obligatorios.";
            return RedirectToAction(nameof(Editar), new { id = nota.Id });
        }

        _store.ActualizarNota(nota);
        TempData["Ok"] = "Nota actualizada.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        _store.EliminarNota(id);
        TempData["Ok"] = "Nota eliminada.";
        return RedirectToAction(nameof(Index));
    }
}
