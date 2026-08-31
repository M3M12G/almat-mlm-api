# Payments (FreedomPay Result URL)

См. `docs/04_payments.md`, `docs/api-contracts/endpoints.md`
(`POST /payments/webhook`), `docs/06_security.md`.

Вебхук публичный (без JWT). Защита = `pg_sig`. Идемпотентность по
`pg_payment_id` → `purchases.payment_provider_tx_id`.

**Жёстко:** после `paid` BonusEngine **не** вызывать. Только статус покупки.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [freedompay-webhook](issues/01-freedompay-webhook.md) | ready |

## Out of scope

- ISO 20022 / массовые выплаты
- BonusEngine / RankEngine / volume increment
- Смена провайдера
