# Almat MLM API — Agent Instructions

Backend workspace: **.NET 10** ASP.NET Core Web API.

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
5. **TickerQ Dashboard без auth** — `WithBasicAuth` (или эквивалент)
   обязателен на всех окружениях; публичный дашборд запрещён.

## Stack (pilot)

- **KISS + YAGNI + OSS** (ADR-0004) — BCL/Microsoft first.
- Controllers + services + EF Core + Npgsql, **TickerQ** (EF / Postgres +
  SignalR-дашборд с Basic Auth), **Mapster** для DTO.
- **Не ставить на старте:** MediatR, AutoMapper, Hangfire, Quartz.NET,
  MassTransit/Kafka, Redis.

## Config

- Postgres: `ConnectionStrings__Default` (env / user-secrets) — не хардкодить
  прод-строку в `appsettings.json`.
- TickerQ dashboard password: `TickerQ__Dashboard__Password`.

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
