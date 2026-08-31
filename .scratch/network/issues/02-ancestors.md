# GET /network/ancestors

Type: task
Status: ready
Blocked by:

## Goal

Цепочка вверх от текущего пользователя, максимум 10 уровней.

## Canon

См. `docs/api-contracts/endpoints.md`, `docs/db/queries_recursive.sql`
(обход ВВЕРХ), `docs/02_network_model.md`.

## Scope

- `[Authorize]`; корень = current user, не произвольный id
- CTE через `FromSqlInterpolated` (параметры, не конкатенация)
- DTO: id, level, rank — без PII лишнего (email не обязателен)

## Out of scope

- Admin-обход чужого дерева (если понадобится — отдельный тикет)
- Bonus calculation

## Done

- [ ] Глубина обрезана на 10
- [ ] Линейно по глубине, без down-scan

## Comments
