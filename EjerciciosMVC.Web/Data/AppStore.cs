using EjerciciosMVC.Web.Models;

namespace EjerciciosMVC.Web.Data;

/// <summary>
/// Almacen en memoria compartido por los 11 ejercicios.
/// Hace las veces de base de datos para que el proyecto corra sin instalar nada.
/// </summary>
public class AppStore
{
    private readonly object _candado = new();

    private int _tareaId = 1;
    private int _gastoId = 1;
    private int _notaId = 1;
    private int _eventoId = 1;
    private int _reservaId = 1;
    private int _recetaId = 1;
    private int _puntajeId = 1;
    private int _encuestaId = 1;
    private int _opcionId = 1;
    private int _registroId = 1;

    public List<Tarea> Tareas { get; } = new();
    public List<Gasto> Gastos { get; } = new();
    public List<Nota> Notas { get; } = new();
    public List<Evento> Eventos { get; } = new();
    public List<Servicio> Servicios { get; } = new();
    public List<Reserva> Reservas { get; } = new();
    public List<Receta> Recetas { get; } = new();
    public List<PuntajeMemoria> Puntajes { get; } = new();
    public List<Encuesta> Encuestas { get; } = new();
    public List<RegistroCronometro> Registros { get; } = new();

    public static readonly string[] CategoriasGasto =
    {
        "Alimentacion", "Transporte", "Servicios", "Educacion", "Ocio", "Salud", "Otros"
    };

    public AppStore()
    {
        Sembrar();
    }

    private void Sembrar()
    {
        Tareas.Add(new Tarea { Id = _tareaId++, Titulo = "Terminar el taller de MVC", CreadaEn = DateTime.Now.AddDays(-1) });
        Tareas.Add(new Tarea { Id = _tareaId++, Titulo = "Estudiar para el parcial", CreadaEn = DateTime.Now.AddHours(-5) });
        Tareas.Add(new Tarea { Id = _tareaId++, Titulo = "Entregar el proyecto de la materia", Completada = true, CreadaEn = DateTime.Now.AddDays(-3) });

        var hoy = DateTime.Today;
        Gastos.Add(new Gasto { Id = _gastoId++, Descripcion = "Almuerzo en la U", Categoria = "Alimentacion", Monto = 18000, Fecha = hoy });
        Gastos.Add(new Gasto { Id = _gastoId++, Descripcion = "Metro ida y vuelta", Categoria = "Transporte", Monto = 4500, Fecha = hoy });
        Gastos.Add(new Gasto { Id = _gastoId++, Descripcion = "Plan de datos", Categoria = "Servicios", Monto = 35000, Fecha = hoy.AddDays(-2) });
        Gastos.Add(new Gasto { Id = _gastoId++, Descripcion = "Mercado de la semana", Categoria = "Alimentacion", Monto = 85000, Fecha = hoy.AddDays(-4) });

        Notas.Add(new Nota { Id = _notaId++, Titulo = "Ideas para el proyecto", Contenido = "Hacer una app con MVC y guardar todo en memoria.", Categoria = "Universidad" });
        Notas.Add(new Nota { Id = _notaId++, Titulo = "Lista de compras", Contenido = "Pan, huevos, leche y cafe.", Categoria = "Personal" });

        Eventos.Add(new Evento { Id = _eventoId++, Titulo = "Parcial de Programacion Web", Fecha = hoy.AddDays(2), Hora = "08:00", Descripcion = "Repasar controladores y vistas.", RecordatorioMin = 60 });
        Eventos.Add(new Evento { Id = _eventoId++, Titulo = "Entrega del taller MVC", Fecha = hoy.AddDays(5), Hora = "23:59", Descripcion = "Subir el proyecto a la plataforma.", RecordatorioMin = 1440 });
        Eventos.Add(new Evento { Id = _eventoId++, Titulo = "Reunion con el equipo", Fecha = hoy, Hora = "14:00", Descripcion = "Definir las pantallas de la app.", RecordatorioMin = 30 });

        Servicios.Add(new Servicio { Id = 1, Nombre = "Corte de cabello", Precio = 25000, DuracionMin = 45 });
        Servicios.Add(new Servicio { Id = 2, Nombre = "Consulta general", Precio = 60000, DuracionMin = 30 });
        Servicios.Add(new Servicio { Id = 3, Nombre = "Masaje relajante", Precio = 50000, DuracionMin = 60 });

        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Arepas con huevo", TipoComida = "Desayuno", Autor = "Abuela", Ingredientes = "2 arepas\n2 huevos\nsal\nmantequilla", Pasos = "1. Calentar las arepas.\n2. Freir los huevos.\n3. Servir con mantequilla." });
        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Bandeja paisa", TipoComida = "Almuerzo", Autor = "Restaurante de la esquina", Ingredientes = "frijoles\narroz\ncarne molida\nchorizo\nhuevo\nplatano", Pasos = "1. Cocinar los frijoles.\n2. Preparar el arroz.\n3. Freir la carne y el chorizo.\n4. Servir todo junto." });
        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Ajiaco santafereño", TipoComida = "Almuerzo", Autor = "Mama", Ingredientes = "papa criolla\npapa pastusa\npollo\nmazorca\ncrema\nguascas", Pasos = "1. Cocinar el pollo.\n2. Agregar las papas y la mazorca.\n3. Servir con crema y guascas." });
        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Ensalada de frutas", TipoComida = "Postre", Autor = "Chef", Ingredientes = "banano\nmanzana\npapaya\nleche condensada", Pasos = "1. Picar la fruta.\n2. Mezclar.\n3. Agregar leche condensada." });
        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Pasta al pesto", TipoComida = "Cena", Autor = "Chef", Ingredientes = "pasta\nalbahaca\naceite de oliva\najo\nqueso parmesano", Pasos = "1. Cocinar la pasta.\n2. Licuar albahaca, aceite y ajo.\n3. Mezclar y servir." });
        Recetas.Add(new Receta { Id = _recetaId++, Nombre = "Limonada de coco", TipoComida = "Bebida", Autor = "Tienda del barrio", Ingredientes = "limon\nleche de coco\nazucar\nhielo", Pasos = "1. Licuar todo.\n2. Servir bien frio." });

        Puntajes.Add(new PuntajeMemoria { Id = _puntajeId++, Nombre = "Sara", Movimientos = 22, Segundos = 75, Dificultad = "facil", Fecha = DateTime.Now.AddHours(-2) });
        Puntajes.Add(new PuntajeMemoria { Id = _puntajeId++, Nombre = "Jonas", Movimientos = 28, Segundos = 90, Dificultad = "medio", Fecha = DateTime.Now.AddHours(-1) });
        Puntajes.Add(new PuntajeMemoria { Id = _puntajeId++, Nombre = "Valentina", Movimientos = 34, Segundos = 120, Dificultad = "dificil", Fecha = DateTime.Now.AddMinutes(-30) });

        Encuestas.Add(new Encuesta
        {
            Id = _encuestaId++,
            Pregunta = "¿Que framework usas mas para proyectos web?",
            Opciones = new List<Opcion>
            {
                new() { Id = _opcionId++, Texto = "ASP.NET Core MVC", Votos = 7 },
                new() { Id = _opcionId++, Texto = "Django", Votos = 4 },
                new() { Id = _opcionId++, Texto = "Laravel", Votos = 3 },
                new() { Id = _opcionId++, Texto = "Express", Votos = 2 }
            }
        });

        Registros.Add(new RegistroCronometro { Id = _registroId++, Nombre = "Estudio 25 min", Tiempo = "25:00.00", Vueltas = 1, Fecha = DateTime.Now.AddHours(-3) });
    }

    // ---------------- Tareas ----------------
    public void AgregarTarea(string titulo)
    {
        lock (_candado)
        {
            Tareas.Add(new Tarea { Id = _tareaId++, Titulo = titulo.Trim() });
        }
    }

    public void AlternarTarea(int id)
    {
        lock (_candado)
        {
            var tarea = Tareas.FirstOrDefault(t => t.Id == id);
            if (tarea is not null) tarea.Completada = !tarea.Completada;
        }
    }

    public void EliminarTarea(int id)
    {
        lock (_candado)
        {
            Tareas.RemoveAll(t => t.Id == id);
        }
    }

    public void LimpiarCompletadas()
    {
        lock (_candado)
        {
            Tareas.RemoveAll(t => t.Completada);
        }
    }

    // ---------------- Gastos ----------------
    public void AgregarGasto(Gasto gasto)
    {
        lock (_candado)
        {
            gasto.Id = _gastoId++;
            Gastos.Add(gasto);
        }
    }

    public void EliminarGasto(int id)
    {
        lock (_candado)
        {
            Gastos.RemoveAll(g => g.Id == id);
        }
    }

    // ---------------- Notas ----------------
    public void AgregarNota(Nota nota)
    {
        lock (_candado)
        {
            nota.Id = _notaId++;
            nota.ActualizadaEn = DateTime.Now;
            Notas.Add(nota);
        }
    }

    public void ActualizarNota(Nota nota)
    {
        lock (_candado)
        {
            var actual = Notas.FirstOrDefault(n => n.Id == nota.Id);
            if (actual is null) return;
            actual.Titulo = nota.Titulo;
            actual.Contenido = nota.Contenido;
            actual.Categoria = nota.Categoria;
            actual.ActualizadaEn = DateTime.Now;
        }
    }

    public void EliminarNota(int id)
    {
        lock (_candado)
        {
            Notas.RemoveAll(n => n.Id == id);
        }
    }

    // ---------------- Eventos ----------------
    public void AgregarEvento(Evento evento)
    {
        lock (_candado)
        {
            evento.Id = _eventoId++;
            Eventos.Add(evento);
        }
    }

    public void ActualizarEvento(Evento evento)
    {
        lock (_candado)
        {
            var actual = Eventos.FirstOrDefault(e => e.Id == evento.Id);
            if (actual is null) return;
            actual.Titulo = evento.Titulo;
            actual.Fecha = evento.Fecha;
            actual.Hora = evento.Hora;
            actual.Descripcion = evento.Descripcion;
            actual.RecordatorioMin = evento.RecordatorioMin;
        }
    }

    public void EliminarEvento(int id)
    {
        lock (_candado)
        {
            Eventos.RemoveAll(e => e.Id == id);
        }
    }

    // ---------------- Reservas ----------------
    public bool EstaOcupada(DateTime fecha, string hora) =>
        Reservas.Any(r => r.Fecha.Date == fecha.Date && r.Hora == hora);

    public bool AgregarReserva(int servicioId, DateTime fecha, string hora, string cliente)
    {
        lock (_candado)
        {
            if (EstaOcupada(fecha, hora)) return false;
            Reservas.Add(new Reserva
            {
                Id = _reservaId++,
                ServicioId = servicioId,
                Fecha = fecha.Date,
                Hora = hora,
                Cliente = cliente.Trim(),
                Confirmada = false
            });
            return true;
        }
    }

    public void ConfirmarReserva(int id)
    {
        lock (_candado)
        {
            var reserva = Reservas.FirstOrDefault(r => r.Id == id);
            if (reserva is not null) reserva.Confirmada = true;
        }
    }

    public void CancelarReserva(int id)
    {
        lock (_candado)
        {
            Reservas.RemoveAll(r => r.Id == id);
        }
    }

    public string NombreServicio(int id) =>
        Servicios.FirstOrDefault(s => s.Id == id)?.Nombre ?? "Servicio";

    // ---------------- Recetas ----------------
    public void AgregarReceta(Receta receta)
    {
        lock (_candado)
        {
            receta.Id = _recetaId++;
            Recetas.Add(receta);
        }
    }

    public void AlternarGuardada(int id)
    {
        lock (_candado)
        {
            var receta = Recetas.FirstOrDefault(r => r.Id == id);
            if (receta is not null) receta.Guardada = !receta.Guardada;
        }
    }

    public void MarcarCompartida(int id)
    {
        lock (_candado)
        {
            var receta = Recetas.FirstOrDefault(r => r.Id == id);
            if (receta is not null) receta.Compartida = true;
        }
    }

    // ---------------- Memoria ----------------
    public void AgregarPuntaje(PuntajeMemoria puntaje)
    {
        lock (_candado)
        {
            puntaje.Id = _puntajeId++;
            puntaje.Fecha = DateTime.Now;
            Puntajes.Add(puntaje);
        }
    }

    // ---------------- Encuestas ----------------
    public void AgregarEncuesta(string pregunta, IEnumerable<string> opciones)
    {
        lock (_candado)
        {
            var encuesta = new Encuesta { Id = _encuestaId++, Pregunta = pregunta.Trim() };
            foreach (var texto in opciones.Where(o => !string.IsNullOrWhiteSpace(o)))
            {
                encuesta.Opciones.Add(new Opcion { Id = _opcionId++, Texto = texto.Trim() });
            }
            Encuestas.Add(encuesta);
        }
    }

    public void Votar(int encuestaId, int opcionId)
    {
        lock (_candado)
        {
            var encuesta = Encuestas.FirstOrDefault(e => e.Id == encuestaId);
            var opcion = encuesta?.Opciones.FirstOrDefault(o => o.Id == opcionId);
            if (opcion is not null) opcion.Votos++;
        }
    }

    // ---------------- Cronometro ----------------
    public void AgregarRegistro(string nombre, string tiempo, int vueltas)
    {
        lock (_candado)
        {
            Registros.Add(new RegistroCronometro
            {
                Id = _registroId++,
                Nombre = string.IsNullOrWhiteSpace(nombre) ? "Sesion" : nombre.Trim(),
                Tiempo = tiempo,
                Vueltas = vueltas
            });
        }
    }
}
