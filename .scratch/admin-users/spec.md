# Admin users

См. `docs/api-contracts/endpoints.md` (Админка: `/admin/users`),
`docs/02_network_model.md` (циклы при смене `sponsor_id`),
`docs/07_open_questions.md` (смена спонсора).

Happy path: спонсор **не** меняется. Админский PATCH — safety net + cycle check.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [list-users](issues/01-list-users.md) | ready |
| 02 | [patch-sponsor-guard](issues/02-patch-sponsor-guard.md) | ready |

## Out of scope

- Смена спонсора как пользовательская операция
- Правка рангов / бонусов из этого слайса
