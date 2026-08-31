# Network

См. `docs/02_network_model.md`, `docs/adr/0001-adjacency-list-for-network-storage.md`,
`docs/db/queries_recursive.sql`, `docs/api-contracts/endpoints.md` (Сеть).

Дерево — adjacency list (`users.sponsor_id`). Обход вниз на лету для рангов —
**не** делать; только ограниченный tree для ЛК.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [cycle-guard](issues/01-cycle-guard.md) | ready |
| 02 | [ancestors](issues/02-ancestors.md) | ready |
| 03 | [tree](issues/03-tree.md) | ready |

## Out of scope

- `GET /network/stats` (агрегаты объёма — после оплаченных покупок)
- Down-scan всей команды для RankEngine (модуль `ranks`)
