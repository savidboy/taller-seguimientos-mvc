using System.Text.Json;
using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers;

public class MemoriaController : Controller
{
    private static readonly Dictionary<string, string[]> Barajas = new()
    {
        ["facil"] = new[] { "🍎", "🍌", "🍇", "🍓", "🍉", "🍍" },
        ["medio"] = new[] { "🍎", "🍌", "🍇", "🍓", "🍉", "🍍", "🥝", "🍒" },
        ["dificil"] = new[] { "🍎", "🍌", "🍇", "🍓", "🍉", "🍍", "🥝", "🍒", "🥑", "🍑" }
    };

    private readonly AppStore _store;

    public MemoriaController(AppStore store) => _store = store;

    public IActionResult Index(string dificultad = "facil")
    {
        if (!Barajas.ContainsKey(dificultad))
        {
            dificultad = "facil";
        }

        var emojis = Barajas[dificultad].SelectMany(e => new[] { e, e }).ToArray();

        for (var i = emojis.Length - 1; i > 0; i--)
        {
            var j = Random.Shared.Next(i + 1);
            (emojis[i], emojis[j]) = (emojis[j], emojis[i]);
        }

        var modelo = new MemoriaViewModel
        {
            Dificultad = dificultad,
            EmojisJson = JsonSerializer.Serialize(emojis),
            Puntajes = _store.Puntajes
                .Where(p => p.Dificultad == dificultad)
                .OrderBy(p => p.Movimientos)
                .ThenBy(p => p.Segundos)
                .Take(10)
                .ToList()
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GuardarPuntaje(string nombre, int movimientos, int segundos, string dificultad)
    {
        if (!Barajas.ContainsKey(dificultad))
        {
            dificultad = "facil";
        }

        _store.AgregarPuntaje(new PuntajeMemoria
        {
            Nombre = string.IsNullOrWhiteSpace(nombre) ? "Anonimo" : nombre.Trim(),
            Movimientos = Math.Clamp(movimientos, 1, 9999),
            Segundos = Math.Clamp(segundos, 0, 86400),
            Dificultad = dificultad
        });

        TempData["Ok"] = "Puntaje guardado.";
        return RedirectToAction(nameof(Index), new { dificultad });
    }
}
