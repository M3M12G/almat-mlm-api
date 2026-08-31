# GET /admin/users

Type: task
Status: ready
Blocked by:

## Goal

Список пользователей с поиском (email / referral_code), пагинация.

## Canon

См. `docs/api-contracts/endpoints.md`. Не логировать PII на Information.

## Scope

- Admin policy (как в wallet/02)
- DTO без password hash / IIN
- Фильтр + page/pageSize с cap

## Out of scope

- Экспорт Excel
- Смена ранга

## Done

- [ ] Не-админ 403
- [ ] Нет entity leakage

## Comments
