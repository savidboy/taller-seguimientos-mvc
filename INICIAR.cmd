@echo off
chcp 65001 >nul
title Ejercicios MVC
cd /d "%~dp0EjerciciosMVC.Web"
start "Servidor MVC" cmd /k "dotnet run --urls http://localhost:5080"
echo Iniciando el servidor...
timeout /t 12 /nobreak >nul
start "" http://localhost:5080
