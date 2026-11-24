using System.Security.Cryptography;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class ContrasenasController : Controller
{
    private const string Mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Minusculas = "abcdefghijklmnopqrstuvwxyz";
    private const string Numeros = "0123456789";
    private const string Simbolos = "!@#$%^&*()-_=+[]{};:,.?";

    public IActionResult Index() => View(new ContrasenaViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Generar(ContrasenaViewModel modelo)
    {
        modelo.Longitud = Math.Clamp(modelo.Longitud, 4, 64);

        var conjuntos = new List<string>();
        if (modelo.Mayusculas) conjuntos.Add(Mayusculas);
        if (modelo.Minusculas) conjuntos.Add(Minusculas);
        if (modelo.Numeros) conjuntos.Add(Numeros);
        if (modelo.Simbolos) conjuntos.Add(Simbolos);

        if (conjuntos.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Selecciona al menos un tipo de caracter.");
            return View("Index", modelo);
        }

        var todos = string.Concat(conjuntos);
        var caracteres = new List<char>();

        foreach (var conjunto in conjuntos)
        {
            caracteres.Add(conjunto[RandomNumberGenerator.GetInt32(conjunto.Length)]);
        }

        while (caracteres.Count < modelo.Longitud)
        {
            caracteres.Add(todos[RandomNumberGenerator.GetInt32(todos.Length)]);
        }

        for (var i = caracteres.Count - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1);
            (caracteres[i], caracteres[j]) = (caracteres[j], caracteres[i]);
        }

        modelo.Resultado = new string(caracteres.Take(modelo.Longitud).ToArray());
        modelo.Entropia = Math.Round(modelo.Longitud * Math.Log2(todos.Length), 1);
        modelo.Nivel = modelo.Entropia < 40 ? "Debil" :
                       modelo.Entropia < 60 ? "Aceptable" :
                       modelo.Entropia < 80 ? "Fuerte" : "Muy fuerte";

        return View("Index", modelo);
    }
}
