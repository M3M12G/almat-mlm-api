# Register with referral code

Type: task
Status: ready
Blocked by:

## Goal

`POST /api/v1/auth/register`: создать пользователя, привязать спонсора по
`referral_code`, выдать сессию.

## Canon

См. `docs/api-contracts/endpoints.md` (Auth), `docs/00_overview.md`,
`docs/02_network_model.md`. Сущность `User` (`SponsorId`, `ReferralCode`).

## Scope

- Email + password + `referral_code` (обязателен на пилоте, кроме оговорённого root)
- Резолв спонсора по `ReferralCode`; 404/validation если код неизвестен
- Уникальный `ReferralCode` новому пользователю
- Password hash (ASP.NET Identity hasher / эквивалент BCL)
- `[AllowAnonymous]` только на register/login; остальное `[Authorize]`
- ProblemDetails на ошибки валидации

## Out of scope

- Смена `sponsor_id` после insert
- SMS verify
- Бонусы за регистрацию

## Done

- [ ] Контрактный путь, не `api/Identity`
- [ ] Спонсор пишется один раз; self-sponsor невозможен (`id != sponsor_id`)
- [ ] Тест: неизвестный referral → 400/404; успешная регистрация создаёт ребро дерева

## Comments
