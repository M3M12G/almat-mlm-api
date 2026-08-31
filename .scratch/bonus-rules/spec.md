# Bonus rules seed (data, not engine)

См. `docs/adr/0003-bonus-rules-as-config-not-code.md`, `docs/03_bonus_engine.md`.
Таблица `bonus_rules` и сущность `BonusRule` уже есть. Движка нет — только данные.

Проценты/пороги **только** в `config_json`, не в C#.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [seed-config-json](issues/01-seed-config-json.md) | ready |

## Out of scope

- Интерпретация правил / начисления
- `PATCH /admin/bonus-rules/{code}` (денежный admin — отдельный слайс после engine)
