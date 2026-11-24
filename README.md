# Taller Seguimientos

Programación de Software  
Samuel David Arrieta Oñate

Los 11 ejercicios del taller están en un solo proyecto de ASP.NET Core MVC (.NET 10),
con la plantilla NiceAdmin que vimos en clase.

## Cómo ejecutar

Doble clic en `INICIAR.cmd` y esperar a que abra el navegador en
<http://localhost:5080>. Para apagarlo, cerrar la ventana del servidor.

También se puede ejecutar desde la consola:

```bash
dotnet run --project EjerciciosMVC.Web
```

Se necesita el SDK de .NET 10.

## Ejercicios

1. Lista de tareas
2. Calculadora de propinas
3. Contraseñas seguras
4. Gestor de gastos
5. Sistema de reservas
6. Gestor de notas
7. Calendario de eventos
8. Plataforma de recetas
9. Juego de memoria
10. Plataforma de encuestas
11. Cronómetro online

## Organización

- `Controllers`: un controlador por ejercicio, más el inicio que agrupa la lista.
- `Models`: clases de datos y modelos de las vistas.
- `Views`: páginas Razor de cada ejercicio.
- `Data/AppStore.cs`: almacén en memoria con los datos de ejemplo.
- `wwwroot/assets`: estilos y scripts de la plantilla.

Los datos se guardan en memoria, así que al cerrar el programa se vuelven a los datos
de ejemplo. No hay que instalar ni configurar una base de datos.
