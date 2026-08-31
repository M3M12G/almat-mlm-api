# Seed bonus_rules.config_json

Type: task
Status: ready
Blocked by:

## Goal

Идемпотентный seed строк правил (Direct / Unilevel / Matching / Rank / Pool)
с цифрами со слайдов. Движок не реализовывать.

## Canon

См. ADR-0003, `docs/03_bonus_engine.md`, `docs/raw/images/`.
`SeedDbContext` — тот же идемпотентный стиль, что packages/ranks.

## Scope

- JSON: Direct 10/20/30; Unilevel 10 levels × 2%; Matching 10% one generation;
  pool 2% world TO + points 1/3/7/15; activity placeholder
  `{ "period": "calendar_month", "timezone": "Asia/Almaty", "minAmount": 0 }`
- `active_from` = now; `active_to` null
- Не хардкодить те же цифры в C# сервисах (сервисов ещё нет)

## Out of scope

- Admin PATCH правил
- Начисления

## Done

- [ ] Повторный запуск API не дублирует rules по `code`
- [ ] Цифры только в JSON колонке

## Comments
