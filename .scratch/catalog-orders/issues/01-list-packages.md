# GET /catalog/packages

Type: task
Status: ready
Blocked by:

## Goal

Отдать три пакета из seed: START 18000 / BUSINESS 54000 / PREMIUM 162000.

## Canon

См. `docs/api-contracts/endpoints.md`, `docs/03_bonus_engine.md` (Пакеты),
`SeedDbContext`.

## Scope

- `[Authorize]` или public витрина — зафиксировать `[Authorize]` для пилота
  (витрина после логина), либо `[AllowAnonymous]` если лендинг должен видеть цены;
  выбрать одно и пометить атрибутом явно
- DTO: id, name, price, description
- Не создавать пакеты в хендлере — только читать seed

## Out of scope

- Доп. товары
- Админ CRUD пакетов

## Done

- [ ] После `dotnet run` три пакета, без дублей seed

## Comments
