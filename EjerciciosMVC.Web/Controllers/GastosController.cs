using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class GastosController : Controller
{
    private readonly AppStore _store;

    public GastosController(AppStore store) => _store = store;

    public IActionResult Index(string? categoria, int? mes, int? anio)
    {
        var hoy = DateTime.Today;
        var m = Math.Clamp(mes ?? hoy.Month, 1, 12);
        var a = Math.Clamp(anio ?? hoy.Year, 2000, 2100);

        var consulta = _store.Gastos.Where(g => g.Fecha.Month == m && g.Fecha.Year == a);

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            consulta = consulta.Where(g => g.Categoria == categoria);
        }

        var lista = consulta.OrderByDescending(g => g.Fecha).ThenByDescending(g => g.Id).ToList();

        var modelo = new GastosViewModel
        {
            Gastos = lista,
            Categorias = AppStore.CategoriasGasto.ToList(),
            CategoriaFiltro = categoria,
            Mes = m,
            Anio = a,
            TotalMes = lista.Sum(g => g.Monto),
            PorCategoria = lista
                .GroupBy(g => g.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Monto))
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Agregar(Gasto gasto)
    {
        if (string.IsNullOrWhiteSpace(gasto.Descripcion))
        {
            TempData["Error"] = "Escribe una descripcion del gasto.";
            return RedirectToAction(nameof(Index));
        }

        if (gasto.Monto <= 0)
        {
            TempData["Error"] = "El monto debe ser mayor que cero.";
            return RedirectToAction(nameof(Index));
        }

        if (gasto.Fecha == default)
        {
            gasto.Fecha = DateTime.Today;
        }

        gasto.Descripcion = gasto.Descripcion.Trim();
        gasto.Categoria = string.IsNullOrWhiteSpace(gasto.Categoria) ? "Otros" : gasto.Categoria;

        _store.AgregarGasto(gasto);
        TempData["Ok"] = "Gasto registrado correctamente.";

        return RedirectToAction(nameof(Index), new { mes = gasto.Fecha.Month, anio = gasto.Fecha.Year });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id, int mes, int anio)
    {
        _store.EliminarGasto(id);
        TempData["Ok"] = "Gasto eliminado.";
        return RedirectToAction(nameof(Index), new { mes, anio });
    }
}
