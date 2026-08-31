# Cycle guard on sponsor_id

Type: task
Status: ready
Blocked by:

## Goal

Любая запись `sponsor_id` (register + admin patch) не создаёт цикл в графе.

## Canon

См. `docs/02_network_model.md`, `docs/db/queries_recursive.sql`
(блок «Проверка на цикл»), ADR-0001.

## Scope

- Сервис, вызываемый перед insert/update `sponsor_id`
- Self-sponsor уже ловит CHECK; нужны циклы A→B→C→A
- Отказ → Result/ProblemDetails, не exception для ожидаемого кейса

## Out of scope

- Перенос целой ветки
- UI

## Done

- [ ] Тест: назначение предка своим спонсором → reject
- [ ] Тест: валидный спонсор вне цепочки → ok
- [ ] Один код path для register и admin patch (не дублировать CTE)

## Comments
