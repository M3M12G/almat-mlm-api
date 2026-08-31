# Ledger + Direct Bonus

Type: task
Status: blocked
Blocked by: 01

## Goal

На `paid` покупки начислить Direct Bonus в append-only `bonus_transactions`.
Проценты только из `bonus_rules.config_json`.

## Canon

См. `docs/03_bonus_engine.md` §1, ADR-0003, `docs/TECH_SPEC.md` §3.3.
`BonusTransaction` уже есть.

## Scope

- Триггер: переход purchase → `paid` (из payments; сейчас webhook **не** зовёт
  engine — подключить здесь)
- N-й лично приглашённый плательщика: 10/20/30% от amount
- Нумерация ЛП — по порядку первой **оплаченной** покупки каждого ЛП
- Идемпотентность: повтор webhook не двоит строки ledger
- Баланс не хранить на `User`

## Out of scope

- Unilevel / Matching / Rank / Pool
- Хардкод % в C#

## Stop

Не начинать без resolved `01` **и** явного OK человека (`AGENTS.md`).

## Done

- [ ] 1-й / 2-й / 3-й ЛП дают разные % по конфигу
- [ ] Replay paid не дублирует Direct
- [ ] Тесты на фикстуре дерева

## Comments
