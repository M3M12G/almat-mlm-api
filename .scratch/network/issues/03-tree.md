# GET /network/tree

Type: task
Status: ready
Blocked by:

## Goal

Дерево вниз от текущего пользователя на N уровней для ЛК (не полный down-scan
компании).

## Canon

См. `docs/api-contracts/endpoints.md`, `docs/02_network_model.md`.
Полный descendants CTE в `queries_recursive.sql` — **не** для этого endpoint.

## Scope

- Query param `depth` с жёстким max (например 3–5 для ЛК)
- Только потомки current user
- Узлы: id, referral_code / display, children[]; без объёмов команды, если не
  считаются инкрементально

## Out of scope

- `GET /network/stats`
- Rank composition
- xyflow (это web)

## Done

- [ ] Depth cap в коде, не доверять клиенту
- [ ] Пользователь не видит чужие ветки

## Comments
