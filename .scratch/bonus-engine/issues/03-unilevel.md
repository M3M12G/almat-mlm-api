# Unilevel Bonus

Type: task
Status: blocked
Blocked by: 01, 02

## Goal

До 10 уровней вверх по 2% каждому **активному** предку. Суммируется с Direct.

## Canon

См. `docs/03_bonus_engine.md` §2, `docs/raw/images/Description1.jpeg`,
`docs/db/queries_recursive.sql` (вверх).

## Scope

- Активность: из `config_json` (после `01` — календарный месяц, любая paid покупка)
- Предвычислить `User.IsActivePeriod` до правила, не ad hoc внутри начисления
  (`03_bonus_engine.md`)
- Уровень 1 = личный спонсор (допущение `01`); он может получить Direct + 2%
- Неактивный уровень пропускается (не «компрессия» на следующего, пока это
  не зафиксировано — **не** компрессовать)

## Out of scope

- Matching
- Смена определения активности без config

## Stop

Не начинать без resolved `01` + human OK.

## Done

- [ ] 10 уровней × 2% на активных; неактивный не получает
- [ ] Идемпотентность вместе с Direct на одном `paid`
- [ ] % из config, не из константы в C#

## Comments
