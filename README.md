# mlm-api

Backend приложения **mlm-api** (.NET 10 ASP.NET Core).  
Репозиторий / локальная папка: `almat-mlm-api`.  
Проект: `Mlm.Api/` (assembly `mlm-api`), solution: `mlm-api.slnx`.

Канон: submodule `docs/` → `almat-mlm-docs`. См. `AGENTS.md`, `docs/TECH_SPEC.md` §3, ADR-0005.

```bash
cd Mlm.Api
dotnet user-secrets set "Quartz:Dashboard:Password" "dev-only"
dotnet run
# Scalar (Development): http://localhost:5108/scalar
# or from repo root:
# dotnet run --project Mlm.Api
```

Старт API накатывает миграции (`DbMigrator`) и идемпотентный seed каталогов
(`packages`, `ranks`). `dotnet ef` из каталога `Mlm.Api/` — design-time factories
читают `appsettings.json` из cwd.

## Database

| Путь | Роль |
|---|---|
| `Mlm.Api/Data/AppDbContext.cs` | Домен + Data Protection keys |
| `Mlm.Api/Data/AuditableEntity.cs` | `created_at` / `created_by` / `updated_at` / `updated_by` |
| `Mlm.Api/Data/Migrations/` | Миграции `AppDbContext` → `__ef_migrations_history` |
| `Mlm.Api/Data/QuartzDbContext.cs` | Пустая модель JobStore |
| `Mlm.Api/Data/QuartzMigrations/` | Миграции Quartz → `__ef_migrations_history_quartz` |
| `Mlm.Api/Data/Scripts/0002_quartz_postgres.sql` | Официальный DDL Quartz.NET Postgres |
| `Mlm.Api/Data/DbMigrator.cs` | `MigrateAsync` обоих контекстов при старте |
| `Mlm.Api/Data/SeedDbContext.cs` | Идемпотентный seed пакетов и рангов |
| `Mlm.Api/Modules/Audit/AuditableSaveChangesInterceptor.cs` | Штампы + колоночный diff в `audit_log` |

Генерация миграций (схема = сущности, не руками):

```bash
cd Mlm.Api
dotnet ef migrations add <Name> --context AppDbContext
# Quartz — только при смене vendor SQL, затем Sql() в Up():
dotnet ef migrations add <Name> --context QuartzDbContext
```

Накат: запустить API. CLI запасной (без seed):

```bash
cd Mlm.Api
dotnet ef database update --context AppDbContext
dotnet ef database update --context QuartzDbContext
```

Локальный reset (прода ещё нет):

```bash
cd Mlm.Api
dotnet ef database drop --force --context AppDbContext
dotnet run   # migrate + seed
```

Дамп DDL: `dotnet ef migrations script --idempotent --context AppDbContext`.  
Схема-канон не в `docs/db/schema.sql` — см. `docs/db/README.md`.
