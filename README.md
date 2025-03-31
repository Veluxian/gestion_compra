Gesti�n de Compras API
Esta API REST administra un sistema de gesti�n de �rdenes de compra, permitiendo la creaci�n, modificaci�n, eliminaci�n y consulta de �rdenes y productos en una base de datos SQL Server.

Tecnolog�as utilizadas
.NET 7.0
SQL Server (Preferiblemente con una instancia SQLEXPRESS)
Entity Framework Core
Visual Studio 2022
Swagger

Instalaci�n

Clonar el repositorio
Abre una terminal y ejecuta:

git clone https://github.com/Veluxian/gestion_compra.git
cd gestion_compra

Configurar la base de datos
La API est� configurada para utilizar una base de datos SQL Server local. Si necesitas modificar la instancia, edita el archivo appsettings.json.

Opcionalmente, puedes definir la variable de entorno DB_SERVER:

setx DB_SERVER "NOMBRE_DEL_SERVIDOR\SQLEXPRESS"

Base de datos a utilizar
Tienes dos opciones para esto

Opci�n 1: Restaurar base de datos desde un backup
Abre SQL Server Management Studio (SSMS)
En el explorador de objetos, haz clic derecho en Databases > Restore Database...
Selecciona el archivo GESTION_COMPRA.bak y sigue los pasos para restaurar la base de datos.

Opci�n 2: Crear la base de datos con un script
Abre SQL Server Management Studio (SSMS)
Crea una nueva base de datos llamada GESTION_COMPRA
Ejecuta el script GestionCompraDB.sql para generar las tablas necesarias.

Configurar y ejecutar la API
Abre el proyecto en Visual Studio 2022

Restaura los paquetes NuGet:

dotnet restore

Ejecuta el proyecto con F5 o usa:

dotnet run

Endpoints disponibles
La API usa Swagger para documentar y probar los endpoints. Una vez en ejecuci�n, puedes acceder a:

https://localhost:7082/swagger

Endpoints

�rdenes
POST /api/ordenes/crearorden - Crear una nueva orden

GET /api/ordenes/listar - Obtener todas las �rdenes

GET /api/ordenes/traer/{id} - Obtener una orden por ID

PUT /api/ordenes/modificar/{id} - Modificar una orden

DELETE /api/ordenes/borrar/{id} - Eliminar una orden

Productos
POST /api/productos/crearproducto - Agregar un nuevo producto

GET /api/productos/listarproductos - Listar todos los productos