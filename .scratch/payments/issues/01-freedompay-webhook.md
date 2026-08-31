# FreedomPay Result URL webhook

Type: task
Status: ready
Blocked by:

## Goal

`POST /api/v1/payments/webhook`: проверить `pg_sig`, идемпотентно выставить
покупке `paid`. BonusEngine не трогать.

## Canon

См. `docs/04_payments.md`, `docs/api-contracts/endpoints.md`.
Конфиг: `FreedomPay__MerchantId` / `SecretKey` / `ApiBaseUrl` (user-secrets).

## Scope

- `[AllowAnonymous]` явно; защита = подпись, не JWT
- Невалидный `pg_sig` → `rejected`
- Идемпотентность: повтор с тем же `pg_payment_id` не меняет уже `paid` и
  не создаёт побочных эффектов
- Ответ в формате провайдера (`ok` / `rejected` / `error`), не ProblemDetails JSON
- Staging: режим `Payments__Provider=Mock` допустим (см. 04_payments.md)

## Out of scope

- BonusEngine, RankEngine, `total_team_volume` increment
- ISO 20022 payouts
- Новая EF-миграция без необходимости (поля на `Purchase` уже есть)

## Done

- [ ] Replay webhook не двоит статус и не зовёт бонусы
- [ ] Подпись проверяется; секрета нет в git
- [ ] Аудит мутации заказа (audit interceptor уже пишет SaveChanges)

## Comments
