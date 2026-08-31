# PATCH /admin/users/{id} sponsor guard

Type: task
Status: ready
Blocked by: 01

## Goal

Админ может сменить `sponsor_id` только через cycle-guard. Не часть user flow.

## Canon

См. `docs/02_network_model.md`, `docs/07_open_questions.md` (смена спонсора),
`network/issues/01-cycle-guard.md`.

## Scope

- Использовать тот же cycle service, что register
- Цикл / self → 409/400 ProblemDetails
- Писать в audit (interceptor)

## Out of scope

- Пользовательская смена спонсора
- Перенос всей ветки одним update

## Done

- [ ] Циклический sponsor отклоняется
- [ ] Валидная смена проходит и видна в `/network/ancestors` у ребёнка

## Comments
