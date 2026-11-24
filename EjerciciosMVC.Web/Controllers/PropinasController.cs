using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class PropinasController : Controller
{
    public IActionResult Index() => View(new PropinaViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Calcular(PropinaViewModel modelo)
    {
        if (modelo.Monto <= 0)
        {
            ModelState.AddModelError(string.Empty, "El monto de la cuenta debe ser mayor que cero.");
        }

        if (modelo.Porcentaje < 0 || modelo.Porcentaje > 100)
        {
            ModelState.AddModelError(string.Empty, "El porcentaje debe estar entre 0 y 100.");
        }

        if (modelo.Personas < 1)
        {
            ModelState.AddModelError(string.Empty, "Debe haber al menos una persona.");
        }

        if (!ModelState.IsValid)
        {
            return View("Index", modelo);
        }

        modelo.Personas = Math.Clamp(modelo.Personas, 1, 100);
        modelo.Propina = Math.Round(modelo.Monto * modelo.Porcentaje / 100m, 2);
        modelo.Total = modelo.Monto + modelo.Propina;
        modelo.PorPersona = Math.Round(modelo.Total / modelo.Personas, 2);
        modelo.Calculado = true;

        return View("Index", modelo);
    }
}
