# POST /me/withdrawals

Type: task
Status: ready
Blocked by:

## Goal

Пользователь создаёт заявку на вывод. Статус `pending`. Деньги не двигаются.

## Canon

См. `docs/04_payments.md` (пилот — ручное подтверждение),
`docs/api-contracts/endpoints.md`, `WithdrawalRequest`.

## Scope

- `[Authorize]`
- Сумма > 0; не больше доступного баланса (ledger aggregate; 0 пока нет начислений)
- `GET /me/withdrawals` — список своих заявок
- Result/Validation если средств нет

## Out of scope

- 2FA
- Автовыплата
- Смена статуса пользователем

## Done

- [ ] Нельзя вывести «в минус»
- [ ] Чужие заявки не видны

## Comments
