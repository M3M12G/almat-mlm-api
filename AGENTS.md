# mlm-api — Agent Instructions

Backend application **mlm-api** (.NET 10 ASP.NET Core Web API).  
Repo / local folder: `almat-mlm-api` (имя репозитория не меняется).

**Canonical docs:** git submodule at `docs/` → repo `almat-mlm-docs`  
- Tech spec: `docs/TECH_SPEC.md`  
- Domain: `docs/0*.md` · DDL: `docs/db/schema.sql` · API: `docs/api-contracts/endpoints.md`  
- ADRs: `docs/adr/` (read before changing architecture)  
- MVP deploy: `docs/09_mvp_deployment.md`

Update docs submodule after canon changes:

```bash
cd docs && git pull origin main && cd ..
git add docs && git commit -m "chore: update docs submodule ref"
```

Compensation plan and money paths are data-driven and append-only.

## Не трогать без подтверждения человека

Агент **останавливается и спрашивает**, прежде чем:

1. **EF Core миграции / схема БД** — любые изменения `docs/db/schema.sql`, entity
   configs, `dotnet ef migrations add|remove`.
2. **Логика BonusEngine (денежные расчёты)** — только после закрытия
   `docs/07_open_questions.md` и явного OK.
3. **`bonus_rules.config_json`** — не хардкодить проценты/пороги в коде
   (ADR-0003).
4. **Новый NuGet** вне allow-list в `docs/01_stack.md` / ADR-0004
   (особенно MediatR, AutoMapper, Hangfire).
5. **Quartz Dashboard без auth** — Basic Auth / admin `[Authorize]` обязателен
   на всех окружениях; публичный дашборд запрещён.

## Stack (pilot)

- **KISS + YAGNI + OSS** (ADR-0004) — BCL/Microsoft first.
- Controllers + services + EF Core + Npgsql, **Quartz.NET** (Postgres JobStore +
  OSS dashboard под ASP.NET Basic Auth / admin policy), **Mapster** для DTO.
- **Не ставить на старте:** MediatR, AutoMapper, Hangfire, TickerQ,
  MassTransit/Kafka, Redis.

## Config

- Postgres: `ConnectionStrings__Default` (env / user-secrets) — не хардкодить
  прод-строку в `appsettings.json`.
- Quartz dashboard: `Quartz__Dashboard__Username` / `Quartz__Dashboard__Password`
  (или эквивалент Basic Auth middleware).
- FreedomPay: `FreedomPay__MerchantId`, `FreedomPay__SecretKey`,
  `FreedomPay__ApiBaseUrl` — см. `docs/04_payments.md`.

## Knowledge graphs

- **codegraph** → `.codegraph/` (`codegraph sync` after code lands)
- **graphify** → optional local `graphify-out/`

## Agent skills

### Issue tracker

Local markdown under `.scratch/<feature-slug>/`. Specs **link** to `docs/…`,
never copy-paste canon. See `docs/agents/issue-tracker.md`.

### Domain docs

`docs/adr/` via submodule (path unchanged vs monorepo). See `docs/agents/domain.md`.
Matt Pocock / setup skills: domain docs live at `docs/adr/` — physical source is
the submodule, not a second copy.
