# ApiEcommerce

API REST para la gestión de un ecommerce, construida con ASP.NET Core, Entity Framework Core y SQL Server. El proyecto incluye autenticación con JWT, control de roles, versionado de API, AutoMapper, subida de imágenes de productos y cacheo de respuestas en algunos endpoints.

## Lo que se estuvo trabajando

Durante el desarrollo se fue armando una API enfocada en:

- Gestión de productos, categorías y usuarios.
- Registro e inicio de sesión con JWT.
- Protección de endpoints con autorización por rol `admin`.
- Versionado de API con rutas como `v1` y `v2`.
- Manejo de imágenes de productos en `wwwroot/ProductsImages`.
- Separación por capas con controladores, repositorios, modelos, DTOs y perfiles de mapeo.
- Persistencia con Entity Framework Core y migraciones.

## Tecnologías usadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- AutoMapper
- Swagger / OpenAPI
- API Versioning

## Estructura general

- `Controllers/`: endpoints de la API.
- `Models/`: entidades y DTOs.
- `Repository/`: lógica de acceso a datos.
- `Data/`: contexto de base de datos.
- `Mapping/`: perfiles de AutoMapper.
- `Migrations/`: historial de migraciones de base de datos.
- `Constants/`: nombres de políticas y perfiles de cache.
- `wwwroot/ProductsImages/`: imágenes subidas de productos.

## Funcionalidades principales

### Usuarios

- Registro de usuario.
- Login con generación de token JWT.
- Consulta de usuarios para administradores.

### Productos

- Listado de productos.
- Consulta por id.
- Creación con imagen opcional.
- Actualización.
- Eliminación.
- Búsqueda por categoría.
- Búsqueda por nombre o descripción.
- Compra de producto descontando stock.

### Categorías

- CRUD de categorías.
- Versionado en `v1` y `v2`.
- Cacheo en consultas por id.

## Rutas principales

- `GET /api/v{version}/products`
- `GET /api/v{version}/products/{id}`
- `POST /api/v{version}/products`
- `PUT /api/v{version}/products/{id}`
- `DELETE /api/v{version}/products/{id}`
- `GET /api/v{version}/users`
- `POST /api/v{version}/users`
- `POST /api/v{version}/users/Login`
- `GET /api/v1/categories`
- `GET /api/v2/categories`

## Configuración local

El proyecto usa configuración en `appsettings.json` para:

- `ConnectionStrings:ConexionSql`
- `ApiSettings:SecretKey`

Antes de ejecutar el proyecto, revisa que la cadena de conexión apunte a tu instancia de SQL Server y que la clave secreta JWT sea suficientemente larga.

## Cómo ejecutar

1. Restaurar dependencias.
2. Aplicar migraciones a la base de datos.
3. Ejecutar la API desde Visual Studio o con `dotnet run`.
4. Abrir Swagger para probar los endpoints.

## Notas

- Los endpoints protegidos requieren autenticación JWT.
- Algunas operaciones están limitadas al rol `admin`.
- Las imágenes de productos se guardan físicamente en `wwwroot/ProductsImages` y también se expone su URL pública.
