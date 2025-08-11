# API_Banca

## Descripción
API_Banca es una API RESTful desarrollada en .NET 8 que permite la gestión de usuarios, cuentas y transacciones bancarias. Utiliza una base de datos SQLite y Entity Framework Core para el acceso a datos.

## Requisitos previos
- .NET 8 SDK instalado
- Visual Studio 2022

## Configuración y ejecución
1. **Clonar el repositorio**
2. **Restaurar dependencias:**
	```powershell
	dotnet restore
	```
3. **Compilar el proyecto:**
	```powershell
	dotnet build
	```
4. **Ejecutar la API:**
	```powershell
	dotnet run --project API_Banca/API_Banca.csproj
	```
	La API estará disponible en `https://localhost:5001` o `http://localhost:5000`.

## Configuración de la base de datos
La API utiliza SQLite. El archivo de base de datos `Banca.sqlite` se encuentra en la carpeta principal del proyecto. No se requiere configuración adicional.

## Endpoints principales

### Usuarios (`/api/user`)
- `POST /API/Banca/Users/Create` — Crea un nuevo usuario (necesario para hacer la cuenta)
- `GET /API/Banca/Users/Search/{name}` — Obtiene un usuario por nombre (ve sus datos y al crear la cuenta puede ver su numero asignado)

### Cuentas (`/api/account`)
- `GET /API/Banca/Account/Search/{number}` — Obtiene una cuenta por Numero de cuenta
- `POST /API/Banca/Account/Create` — Crea una nueva cuenta (Con usuario creado)

### Transacciones (`/api/transaction`)
- `GET /API/Banca/Transaction/Search{number}` — Obtiene una transacción por Numero de cuenta
- `POST /API/Banca/Transaction/Create` — Crea una nueva transacción

## Pruebas
Las pruebas unitarias se encuentran en el proyecto `API_Banca.Tests`. Para ejecutarlas:
```powershell
dotnet test API_Banca.Tests/API_Banca.Tests.csproj
```

## Notas adicionales
- En program.cs esta configurado para que la BD se reinicie en cada ejecucion, para evitar eso se puede comentar la linea 28 (dbContext.Database.EnsureDeleted();)

---

**Autor:** rocafi