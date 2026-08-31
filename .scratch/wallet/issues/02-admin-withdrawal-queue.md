# Admin withdrawal queue

Type: task
Status: ready
Blocked by:

## Goal

`GET/PATCH /api/v1/admin/withdrawals`: очередь confirm/reject.

## Canon

См. `docs/api-contracts/endpoints.md` (Админка), `docs/04_payments.md`.

## Scope

- `[Authorize]` + admin policy/role (если роли ещё нет — минимальная policy,
  не оставлять `[AllowAnonymous]`)
- PATCH: `approved` | `rejected`; `ProcessedAt` / `ProcessedBy`
- Не делать банковский перевод — только статус заявки
- Audit уже на SaveChanges

## Out of scope

- ISO 20022
- Пересчёт бонусов

## Done

- [ ] Повторный PATCH уже обработанной заявки → Conflict
- [ ] Не-админ → 403

## Comments
