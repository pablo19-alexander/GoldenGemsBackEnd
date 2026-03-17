# Golden Gems Backend

Backend en C# ASP.NET Core 10.0 para la aplicación Golden Gems.

## Información General

- **Framework**: ASP.NET Core 10.0
- **Base de datos**: PostgreSQL (Npgsql.EntityFrameworkCore)
- **Autenticación**: JWT Bearer (HS256)
- **Documentación API**: Swagger/OpenAPI (Swashbuckle.AspNetCore)
- **ORM**: Entity Framework Core 10.0.2
- **Puerto local**: `http://localhost:5135` (dev), `https://localhost:7286` (HTTPS)

## Estructura del Proyecto

```
GoldenGemsBackEnd/
├── Configurations/
│   └── JwtSettings.cs              # Modelo de configuración JWT
├── Controllers/
│   ├── AuthController.cs           # Login y registro
│   ├── RoleController.cs           # CRUD de roles (Admin)
│   ├── ActionController.cs         # CRUD de acciones (Admin)
│   └── HealthController.cs         # Health check
├── Data/
│   ├── Configurations/
│   │   └── UserConfiguration.cs    # Configuración EF del User
│   └── GoldenGemsDbContext.cs      # DbContext principal
├── DTOs/
│   ├── ApiResponse.cs              # Wrapper genérico de respuesta
│   ├── Admin/
│   │   ├── CreateRoleRequestDto.cs
│   │   ├── RoleResponseDto.cs
│   │   ├── CreateActionRequestDto.cs
│   │   └── ActionResponseDto.cs
│   └── Auth/
│       ├── LoginRequestDto.cs
│       ├── RegisterRequestDto.cs
│       ├── CreateUserRequestDto.cs # DTO para creación admin (pendiente)
│       └── AuthResponseDto.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs  # Manejo global de excepciones
├── Migrations/
│   ├── 20260211022910_InitialCreate.cs
│   └── 20260222202416_AddActionTypeEntity.cs
├── Models/
│   ├── BaseEntity.cs               # Entidad base (Id, CreatedAt, UpdatedAt, IsActive)
│   ├── Security/
│   │   ├── User.cs                 # Usuario (Email, Username, PasswordHash)
│   │   ├── Role.cs                 # Rol
│   │   ├── Actions.cs              # Acción/permiso
│   │   ├── ActionType.cs           # Tipo de acción
│   │   ├── UserRole.cs             # Tabla puente User-Role
│   │   ├── RoleAction.cs           # Tabla puente Role-Action
│   │   ├── Module.cs               # Módulo del sistema
│   │   └── Form.cs                 # Formulario dentro de módulo
│   └── People/
│       ├── Person.cs               # Persona (datos personales, 1:1 con User)
│       ├── DocumentType.cs         # Tipo de documento
│       ├── Contact.cs              # Información de contacto
│       └── Region.cs               # Departamento/Municipio
├── Repositories/
│   ├── IRepository.cs              # Interfaz genérica
│   ├── GenericRepository.cs        # Implementación genérica (CRUD + soft delete)
│   ├── Admin/
│   │   ├── Interfaces/
│   │   │   ├── IRoleRepository.cs
│   │   │   ├── IActionRepository.cs
│   │   │   └── IActionTypeRepository.cs
│   │   ├── RoleRepository.cs
│   │   ├── ActionRepository.cs
│   │   └── ActionTypeRepository.cs
│   └── Auth/
│       ├── Interfaces/
│       │   └── IUserRepository.cs
│       └── UserRepository.cs
├── Services/
│   ├── BaseService.cs              # Servicio base con ILogger
│   ├── Admin/
│   │   ├── Interfaces/
│   │   │   ├── IRoleService.cs
│   │   │   └── IActionService.cs
│   │   └── Services/
│   │       ├── RoleService.cs
│   │       └── ActionService.cs
│   └── Auth/
│       ├── Interfaces/
│       │   ├── IAuthService.cs
│       │   ├── ITokenService.cs
│       │   └── IUserValidationService.cs  # Interfaz definida, sin implementación
│       ├── Models/
│       │   └── TokenResult.cs
│       ├── Services/
│       │   ├── AuthService.cs
│       │   └── JwtTokenService.cs
│       └── Validators/
│           └── PasswordValidator.cs       # Validador estático de contraseñas
├── Info/
│   └── Regiones.xls                # Datos de regiones (referencia)
├── Program.cs                      # Startup y configuración de DI
├── appsettings.json                # Configuración producción
├── appsettings.Development.json    # Configuración desarrollo
├── GoldenGemsBackEnd.http          # Tests HTTP
├── api-tests.http                  # Tests HTTP adicionales
└── GoldenGemsBackEnd.csproj        # Archivo de proyecto
```

## Endpoints API

### Auth (`/api/auth`) — Públicos
| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/register` | Registro público (crea User + Person, asigna rol "User") |
| POST | `/api/auth/login` | Login por email o username, retorna JWT |

### Roles (`/api/role`) — Requiere `[Authorize(Roles = "Admin")]`
| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/role/create` | Crear rol |
| GET | `/api/role/all` | Listar todos los roles |

### Actions (`/api/action`) — Requiere `[Authorize(Roles = "Admin")]`
| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/action/create` | Crear acción (requiere ActionTypeId válido) |
| GET | `/api/action/all` | Listar todas las acciones |

### Health (`/api/health`) — Público
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/health` | Health check con timestamp |

## Arquitectura y Patrones

- **Arquitectura en capas**: Controllers → Services → Repositories → DbContext
- **Repository Pattern**: `GenericRepository<T>` base con operaciones CRUD
- **Soft Delete**: Todas las entidades heredan de `BaseEntity` con campo `IsActive`
- **BaseEntity**: Provee `Id` (Guid), `CreatedAt`, `UpdatedAt`, `IsActive`
- **ApiResponse<T>**: Wrapper estándar para todas las respuestas de API
- **ExceptionHandlingMiddleware**: Captura excepciones no manejadas, retorna JSON estandarizado
- **Inicialización automática**: El rol "User" se crea al iniciar la app si no existe

## Validación de Contraseñas (PasswordValidator)

- Mínimo 8 caracteres
- Al menos 1 mayúscula, 1 minúscula, 1 número, 1 carácter especial (!@#$%^&*)

## Configuración

- **CORS**: Orígenes permitidos: `http://localhost:3000`, `http://localhost:5173`
- **JWT**: Configurado en `appsettings.json` → sección `JwtSettings`
- **PostgreSQL**: Connection string en `ConnectionStrings:DefaultConnection`
- **Nullable reference types**: habilitados
- **Implicit usings**: habilitados

## Dependencias

| Paquete | Versión |
|---------|---------|
| Microsoft.AspNetCore.Authentication.JwtBearer | 10.0.2 |
| Microsoft.AspNetCore.OpenApi | 10.0.2 |
| Microsoft.EntityFrameworkCore | 10.0.2 |
| Microsoft.EntityFrameworkCore.Design | 10.0.2 |
| Npgsql.EntityFrameworkCore.PostgreSQL | 10.0.0 |
| Swashbuckle.AspNetCore | 10.1.0 |

## Estado Actual del Desarrollo

### Completado
- Autenticación JWT (login y registro)
- Gestión de roles (crear, listar) — solo Admin
- Gestión de acciones/permisos (crear, listar) — solo Admin
- Modelo de datos completo (Security + People)
- Middleware de manejo de excepciones
- Health check endpoint
- Inicialización automática del rol "User"

### Pendiente
- **UserValidationService**: Interfaz `IUserValidationService` definida pero sin implementación
- **Endpoint `/api/auth/create`**: Creación de usuarios por Admin (DTO `CreateUserRequestDto` ya existe)
- **Validaciones faltantes en registro**: Unicidad de documento, existencia de DocumentType
- **CRUD completo de entidades**: Module, Form, DocumentType, Contact, Region

## Comandos

```bash
dotnet restore          # Instalar dependencias
dotnet build            # Compilar
dotnet run              # Ejecutar (dev)
dotnet ef database update  # Aplicar migraciones
dotnet ef migrations add <Nombre>  # Crear migración
```

## Git

- Rama principal: `master`
- Commits en español con mensajes descriptivos
