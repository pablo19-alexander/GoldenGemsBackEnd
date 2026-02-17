# Próximos Pasos - Implementación de Creación de Usuarios

## ✅ COMPLETADO - FASE 0: Gestión de Roles y Acciones

### Archivos creados y funcionales:
1. **DTOs Admin**:
   - `CreateRoleRequestDto.cs` - Solicitud para crear rol
   - `RoleResponseDto.cs` - Respuesta del rol
   - `CreateActionRequestDto.cs` - Solicitud para crear acción
   - `ActionResponseDto.cs` - Respuesta de la acción

2. **Repositorios**:
   - `Repositories/Admin/Interfaces/IRoleRepository.cs`
   - `Repositories/Admin/RoleRepository.cs`
   - `Repositories/Admin/Interfaces/IActionRepository.cs`
   - `Repositories/Admin/ActionRepository.cs`

3. **Servicios**:
   - `Services/Admin/Interfaces/IRoleService.cs`
   - `Services/Admin/Services/RoleService.cs`
   - `Services/Admin/Interfaces/IActionService.cs`
   - `Services/Admin/Services/ActionService.cs`

4. **Controladores**:
   - `Controllers/RoleController.cs` - POST/GET /api/role
   - `Controllers/ActionController.cs` - POST/GET /api/action

5. **Inicialización**:
   - Rol "User" se crea automáticamente al iniciar la aplicación

---

## 🔄 EN PROGRESO - FASE 1: Creación de Usuarios

### Parcialmente completado:

1. **DTOs** ✅:
   - `CreateUserRequestDto.cs` - Para admin crear usuarios
   - `RegisterRequestDto.cs` - Actualizado con campos de Persona

2. **Repositorios** ✅:
   - `Repositories/Auth/Interfaces/IUserRepository.cs`
   - `Repositories/Auth/UserRepository.cs`

3. **Validadores** ✅:
   - `Services/Auth/Validators/PasswordValidator.cs`
   - Interfaz: `Services/Auth/Interfaces/IUserValidationService.cs`

### Por completar (crítico):

#### 1. Implementar UserValidationService
**Archivo**: `Services/Auth/Services/UserValidationService.cs`

```csharp
public class UserValidationService : BaseService, IUserValidationService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly GoldenGemsDbContext _context;

    public UserValidationService(
        IUserRepository userRepository,
        IRoleService roleService,
        GoldenGemsDbContext context,
        ILogger<UserValidationService> logger)
        : base(logger)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _context = context;
    }

    // Implementar todos los métodos de IUserValidationService
    // Ver: Services/Auth/Interfaces/IUserValidationService.cs
}
```

#### 2. Actualizar AuthService
**Archivo**: `Services/Auth/Services/AuthService.cs`

Agregar métodos:
- `CreateUserAsAdminAsync(CreateUserRequestDto request, CancellationToken cancellationToken)`
- `CreateUserInternalAsync(...)` - Método privado reutilizable
- `GetDefaultUserRoleAsync(CancellationToken cancellationToken)`
- `AssignRolesToUserAsync(User user, List<Guid> roleIds, CancellationToken cancellationToken)`

Actualizar `RegisterAsync()`:
- Incluir información de Person
- Asignar rol "User" automáticamente
- IsActive siempre true

#### 3. Actualizar AuthController
**Archivo**: `Controllers/AuthController.cs`

Agregar endpoint:
```csharp
/// <summary>
/// Crear usuario como admin (requiere rol Admin)
/// </summary>
[HttpPost("create")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> CreateUser(
    [FromBody] CreateUserRequestDto request,
    CancellationToken cancellationToken)
{
    var result = await _authService.CreateUserAsAdminAsync(request, cancellationToken);
    if (!result.Success)
        return BadRequest(result);
    return CreatedAtAction(nameof(CreateUser), result);
}
```

#### 4. Registrar UserValidationService en Program.cs
```csharp
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
```

#### 5. Crear migraciones
```bash
dotnet ef migrations add AddUserCreationFields
dotnet ef database update
```

#### 6. Actualizar archivos de prueba HTTP
- `/GoldenGemsBackEnd.http`
- `/api-tests.http`

Agregar ejemplos:
```http
### Crear rol User (primero)
POST http://localhost:5000/api/role/create
Authorization: Bearer {ADMIN_TOKEN}
Content-Type: application/json

{
  "name": "Admin",
  "description": "Rol administrador"
}

### Registrar usuario (nuevo)
POST http://localhost:5000/api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "username": "newuser",
  "password": "SecurePass123!",
  "firstName": "John",
  "firstLastName": "Doe",
  "documentTypeId": "GUID_AQUI",
  "documentNumber": "12345678"
}

### Crear usuario como admin (nuevo)
POST http://localhost:5000/api/auth/create
Authorization: Bearer {ADMIN_TOKEN}
Content-Type: application/json

{
  "email": "admin-user@example.com",
  "username": "admin_user",
  "password": "AdminPass123!",
  "firstName": "Jane",
  "firstLastName": "Smith",
  "documentTypeId": "GUID_AQUI",
  "documentNumber": "87654321",
  "roleIds": ["ROLE_GUID_1", "ROLE_GUID_2"],
  "isActive": true
}
```

#### 7. Actualizar CLAUDE.md
Agregar sección: "User Creation Flow"
- Explicar ambos flujos (registro público y admin)
- Listar validaciones
- Mostrar ejemplos

---

## Estado de la compilación

✅ **Compila exitosamente** (sin errores, solo 3 warnings por null references)

```bash
cd /Users/pablosalazar/Documents/GoldenGems/GgBackEnd/GoldenGemsBackEnd
dotnet build
```

---

## Estructura del Proyecto Actualizada

```
GoldenGemsBackEnd/
├── DTOs/
│   ├── Admin/
│   │   ├── CreateRoleRequestDto.cs ✅
│   │   ├── RoleResponseDto.cs ✅
│   │   ├── CreateActionRequestDto.cs ✅
│   │   └── ActionResponseDto.cs ✅
│   └── Auth/
│       ├── RegisterRequestDto.cs ✅ (actualizado)
│       └── CreateUserRequestDto.cs ✅
├── Controllers/
│   ├── AuthController.cs (por actualizar)
│   ├── RoleController.cs ✅
│   └── ActionController.cs ✅
├── Repositories/
│   ├── Admin/
│   │   ├── Interfaces/
│   │   │   ├── IRoleRepository.cs ✅
│   │   │   └── IActionRepository.cs ✅
│   │   ├── RoleRepository.cs ✅
│   │   └── ActionRepository.cs ✅
│   └── Auth/
│       ├── Interfaces/
│       │   └── IUserRepository.cs ✅
│       └── UserRepository.cs ✅
└── Services/
    ├── Admin/
    │   ├── Interfaces/
    │   │   ├── IRoleService.cs ✅
    │   │   └── IActionService.cs ✅
    │   └── Services/
    │       ├── RoleService.cs ✅
    │       └── ActionService.cs ✅
    └── Auth/
        ├── Interfaces/
        │   └── IUserValidationService.cs ✅
        ├── Services/
        │   ├── AuthService.cs (por actualizar)
        │   └── UserValidationService.cs (por crear)
        └── Validators/
            └── PasswordValidator.cs ✅
```

---

## Checklist de Tareas Restantes

- [ ] Crear e implementar `UserValidationService`
- [ ] Actualizar `AuthService` con métodos de creación de usuario
- [ ] Actualizar `AuthController` con endpoint `/api/auth/create`
- [ ] Registrar `IUserValidationService` en Program.cs
- [ ] Crear migraciones de BD
- [ ] Actualizar archivos HTTP de prueba
- [ ] Actualizar documentación CLAUDE.md
- [ ] Compilar sin errores
- [ ] Pruebas manuales:
  - [ ] Crear rol
  - [ ] Registrar usuario (con información completa)
  - [ ] Verificar rol "User" asignado
  - [ ] Crear usuario como admin
  - [ ] Verificar contraseña fuerte
  - [ ] Verificar email/username únicos
  - [ ] Verificar documento único por tipo

---

## Notas Importantes

1. **PasswordValidator**: Ya implementado y funcional. Requiere:
   - Mínimo 8 caracteres
   - 1 mayúscula, 1 minúscula, 1 número, 1 especial

2. **Validación de Documentos**: Debe verificar unicidad en combo (DocumentNumber + DocumentTypeId)

3. **Transaccionalidad**: Usar transacciones para User + Person + UserRoles

4. **Mensajes de Error**: Genéricos por seguridad (no revelar si email existe)

5. **Normalización**: Convertir email/username a lowercase

6. **JWT**: Ya funcional, no requiere cambios

---

## Plan de Continuación

La próxima sesión debe:
1. Crear `UserValidationService` (repositorio de datos y validaciones)
2. Actualizar `AuthService` (métodos de creación)
3. Actualizar `AuthController` (endpoint POST /api/auth/create)
4. Crear migraciones
5. Pruebas completas

Tiempo estimado: 60-90 minutos
