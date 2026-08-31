# GET /me profile

Type: task
Status: ready
Blocked by:

## Goal

`GET /api/v1/me`: профиль текущего пользователя (ранг, реферальный код,
`is_active_period`, без утечки entity).

## Canon

См. `docs/api-contracts/endpoints.md` (Личный кабинет). `[Authorize]`.

## Scope

- DTO: id, email, referral_code, rank name/id, sponsor_id (не вся цепочка)
- Баланс: агрегат `bonus_transactions` где `to_user_id = me` и status accrued;
  пока таблица пуста → `0`
- Не возвращать `PasswordHash`, `Iin`, lockout fields

## Out of scope

- История начислений (`/me/bonus-transactions`)
- Rank progress

## Done

- [ ] 401 без cookie
- [ ] Response shape — DTO, не `User` entity

## Comments
