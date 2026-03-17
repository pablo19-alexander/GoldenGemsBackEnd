---
name: gg-db-query
description: >
  Skill para realizar consultas directas a la base de datos PostgreSQL de GoldenGems (GoldenGemsDB).
  Activar SIEMPRE que el usuario mencione: consultar datos, verificar registros, explorar tablas,
  contar registros, buscar usuarios, ver roles, revisar permisos, depurar datos, hacer SELECT,
  INSERT, UPDATE, DELETE, o cualquier operación SQL sobre la base de datos.
  También activar cuando el usuario diga "revisa en la base de datos", "cuantos registros hay",
  "muestra los datos de", "que hay en la tabla", "consulta la DB", "verifica en postgres",
  o cualquier variante que implique acceder a datos persistidos en PostgreSQL.
  No activar para consultas sobre el código fuente C# o la estructura del proyecto (a menos que
  el usuario explícitamente pida comparar el modelo EF con la DB real).
---

# GoldenGems DB Query

Skill para consultar la base de datos PostgreSQL **GoldenGemsDB** del proyecto Golden Gems Backend.

## Conexion

```
Host: localhost
Port: 5432
Database: GoldenGemsDB
Username: postgres
Password: 123
```

### Metodo preferido: psql via Bash

```bash
PGPASSWORD=123 psql -h localhost -p 5432 -U postgres -d GoldenGemsDB -c "TU_QUERY_AQUI"
```

Para queries multilínea o con formato mejorado:
```bash
PGPASSWORD=123 psql -h localhost -p 5432 -U postgres -d GoldenGemsDB -c "TU_QUERY" --pset=format=wrapped
```

### Metodo alternativo: MCP Server

Si el MCP `goldengems_db` esta disponible, usar las tools:
- `mcp__goldengems_db__query` — ejecutar SQL
- `mcp__goldengems_db__list_tables` — listar tablas
- `mcp__goldengems_db__describe_table` — describir estructura de tabla

## Schema de la Base de Datos

### Tablas del modulo Security

| Tabla | Columnas principales | Notas |
|-------|---------------------|-------|
| **Users** | `Id` (uuid PK), `Email` (text, unique), `Username` (text, unique), `PasswordHash` (text) | Credenciales de autenticacion |
| **Roles** | `Id` (uuid PK), `Name` (text), `Description` (text) | Roles del sistema (Admin, User, etc.) |
| **UserRoles** | `Id` (uuid PK), `UserId` (uuid FK→Users), `RoleId` (uuid FK→Roles) | Tabla puente muchos-a-muchos |
| **Actions** | `Id` (uuid PK), `Name` (text), `Description` (text), `Code` (text), `ActionType` (text), `ModuleId` (uuid FK→Modules), `FormId` (uuid FK→Forms) | Permisos/acciones del sistema |
| **RoleActions** | `Id` (uuid PK), `RoleId` (uuid FK→Roles), `ActionId` (uuid FK→Actions) | Tabla puente muchos-a-muchos |
| **Modules** | `Id` (uuid PK), `Code` (text), `Name` (text), `Description` (text), `Icon` (text nullable), `DisplayOrder` (int) | Modulos del sistema |
| **Forms** | `Id` (uuid PK), `Code` (text), `FormReference` (text), `Name` (text), `Description` (text), `Route` (text nullable), `ModuleId` (uuid FK→Modules), `DisplayOrder` (int) | Formularios dentro de modulos |

### Tablas del modulo People

| Tabla | Columnas principales | Notas |
|-------|---------------------|-------|
| **People** | `Id` (uuid PK), `FirstName`, `SecondName`, `FirstLastName`, `SecondLastName` (text), `DocumentNumber` (text), `DocumentTypeId` (uuid FK→DocumentTypes), `ContactId` (uuid FK→Contacts, nullable), `UserId` (uuid FK→Users) | Datos personales, relacion 1:1 con Users |
| **DocumentTypes** | `Id` (uuid PK), `Code` (text), `Name` (text) | Tipos de documento (CC, TI, CE, etc.) |
| **Contacts** | `Id` (uuid PK), `Mobile` (text), `Email` (text), `Address` (text), `Neighborhood` (text), `RegionId` (uuid FK→Regions, nullable) | Informacion de contacto |
| **Regions** | `Id` (uuid PK), `Department` (text), `MunicipalityCode` (text), `MunicipalityName` (text) | Departamentos y municipios de Colombia |

### BaseEntity (todas las tablas)

Todas las tablas heredan estos campos:
- `Id` — uuid, clave primaria
- `CreatedAt` — timestamp with time zone, fecha de creacion
- `UpdatedAt` — timestamp with time zone nullable, ultima modificacion
- `IsActive` — boolean, soft delete (true = activo, false = eliminado logicamente)

### Relaciones (Foreign Keys)

```
Users 1──N UserRoles N──1 Roles
Roles 1──N RoleActions N──1 Actions
Actions N──1 Modules
Actions N──1 Forms
Forms N──1 Modules
People 1──1 Users
People N──1 DocumentTypes
People N──1 Contacts (nullable)
Contacts N──1 Regions (nullable)
```

## Reglas de Seguridad

### SELECT — Libre
Ejecutar sin pedir confirmacion. Siempre incluir `WHERE "IsActive" = true` a menos que el usuario explícitamente pida ver registros inactivos/eliminados.

### INSERT, UPDATE, DELETE — Requieren confirmacion
Antes de ejecutar cualquier operacion de escritura:
1. Mostrar la query exacta que se va a ejecutar
2. Explicar que registros se veran afectados
3. Pedir confirmacion explicita al usuario
4. Para DELETE, preferir siempre soft delete: `UPDATE "Tabla" SET "IsActive" = false WHERE ...`

### Nunca ejecutar
- `DROP TABLE`, `DROP DATABASE`, `TRUNCATE`
- Modificaciones al schema (`ALTER TABLE`, `CREATE TABLE`) — eso se maneja con EF Migrations
- Queries que expongan `PasswordHash` a menos que sea para depuracion solicitada

## Patrones de Consulta Comunes

### Ver usuarios con sus roles
```sql
SELECT u."Id", u."Email", u."Username", r."Name" as "RoleName"
FROM "Users" u
JOIN "UserRoles" ur ON u."Id" = ur."UserId"
JOIN "Roles" r ON ur."RoleId" = r."Id"
WHERE u."IsActive" = true AND ur."IsActive" = true;
```

### Ver personas con datos completos
```sql
SELECT p."FirstName", p."FirstLastName", p."DocumentNumber",
       dt."Name" as "DocType", u."Email", u."Username"
FROM "People" p
JOIN "Users" u ON p."UserId" = u."Id"
JOIN "DocumentTypes" dt ON p."DocumentTypeId" = dt."Id"
WHERE p."IsActive" = true;
```

### Ver permisos de un rol
```sql
SELECT r."Name" as "Rol", a."Name" as "Accion", a."Code", a."ActionType"
FROM "RoleActions" ra
JOIN "Roles" r ON ra."RoleId" = r."Id"
JOIN "Actions" a ON ra."ActionId" = a."Id"
WHERE ra."IsActive" = true;
```

### Contar registros por tabla
```sql
SELECT 'Users' as tabla, COUNT(*) as total FROM "Users" WHERE "IsActive" = true
UNION ALL
SELECT 'Roles', COUNT(*) FROM "Roles" WHERE "IsActive" = true
UNION ALL
SELECT 'People', COUNT(*) FROM "People" WHERE "IsActive" = true;
```

## Formato de Resultados

- Presentar resultados en tablas markdown cuando sean pocos registros
- Para resultados grandes (>20 filas), mostrar resumen + primeras filas
- Siempre indicar el total de registros encontrados
- Si no hay resultados, indicarlo claramente

## Importante: Nombres con comillas dobles

PostgreSQL en este proyecto usa nombres con PascalCase. Siempre envolver nombres de tablas y columnas en comillas dobles:
- Correcto: `SELECT "Email" FROM "Users"`
- Incorrecto: `SELECT Email FROM Users` (esto buscaría en minusculas y fallaria)
