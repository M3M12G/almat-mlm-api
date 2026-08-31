# Bonus Engine (ledger + Direct / Unilevel / Matching)

См. `docs/03_bonus_engine.md`, `docs/TECH_SPEC.md` §3.3, ADR-0003,
`docs/07_open_questions.md`, `docs/raw/images/` (Description1–3).

`AGENTS.md`: BonusEngine **не** писать до закрытия open questions **и** явного OK.

Тикет `01` как раз закрывает вопросы рабочими допущениями со слайдов.
Тикеты `02–04` после `01` всё ещё ждут human OK.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [freeze-working-assumptions](issues/01-freeze-working-assumptions.md) | ready |
| 02 | [ledger-and-direct](issues/02-ledger-and-direct.md) | blocked |
| 03 | [unilevel](issues/03-unilevel.md) | blocked |
| 04 | [matching](issues/04-matching.md) | blocked |

## Out of scope

- RankEngine → `ranks`
- Leadership Pool → `leadership-pool`
- Хардкод процентов в C#
