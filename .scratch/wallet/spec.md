# Wallet / Withdrawals

См. `docs/04_payments.md` (Выплаты партнёрам — пилот),
`docs/api-contracts/endpoints.md` (`/me/withdrawals`, `/admin/withdrawals`).
Сущность `WithdrawalRequest` есть.

Пилот: ручное подтверждение админом. Баланс = агрегат ledger; пока ledger пуст,
заявка должна отклоняться валидацией «недостаточно средств», не падать.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [create-withdrawal](issues/01-create-withdrawal.md) | ready |
| 02 | [admin-withdrawal-queue](issues/02-admin-withdrawal-queue.md) | ready |

## Out of scope

- 2FA перед заявкой (`docs/08_roadmap.md` этап 5)
- KYC / ИИН как жёсткий gate (поле на User есть — не блокировать этот слайс)
- Автовыплаты ISO 20022
