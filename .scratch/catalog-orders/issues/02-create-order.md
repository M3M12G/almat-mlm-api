# POST /orders

Type: task
Status: ready
Blocked by:

## Goal

Создать `Purchase` в статусе `pending` на выбранный пакет. Не помечать `paid`.

## Canon

См. `docs/api-contracts/endpoints.md`, `docs/04_payments.md` (статус только из
webhook), сущность `Purchase`.

## Scope

- Body: `package_id`
- Amount/LP с пакета, не с клиента
- `buyer_id` = current user
- `payment_provider_tx_id` пустой до webhook
- Не вызывать BonusEngine

## Out of scope

- Редирект FreedomPay (может вернуть `order_id` + позже payment init)
- Корзина multi-item

## Done

- [ ] Повторный POST создаёт новый pending (или явная идемпотентность ключом —
      выбрать одно и задокументировать в ответе тикета)
- [ ] Клиент не может подставить чужой amount

## Comments
