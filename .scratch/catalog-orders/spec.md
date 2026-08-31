# Catalog / Orders

См. `docs/api-contracts/endpoints.md` (Каталог / заказы), `docs/03_bonus_engine.md`
(пакеты), `docs/04_payments.md`. Seed пакетов уже в `SeedDbContext`
(START / BUSINESS / PREMIUM). Сущность `Purchase` есть.

Заказ создаётся в `pending`. Переход в `paid` — **только** из payments webhook,
не из этого слайса.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [list-packages](issues/01-list-packages.md) | ready |
| 02 | [create-order](issues/02-create-order.md) | ready |

## Out of scope

- Вызов BonusEngine
- Корзина с несколькими товарами / доп. SKU
- Mock vs FreedomPay init (init платежа — `payments`, если нужен отдельный шаг)
