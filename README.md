# mlm-api

Backend приложения **mlm-api** (.NET 10 ASP.NET Core).  
Репозиторий / локальная папка: `almat-mlm-api`.  
Проект: `Mlm.Api/` (assembly `mlm-api`), solution: `mlm-api.slnx`.

Канон: submodule `docs/` → `almat-mlm-docs`. См. `AGENTS.md`, `docs/TECH_SPEC.md` §3.

```bash
cd Mlm.Api
dotnet user-secrets set "Quartz:Dashboard:Password" "dev-only"
dotnet run
# or from repo root:
# dotnet run --project Mlm.Api
```
