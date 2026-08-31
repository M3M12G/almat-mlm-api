# Matching Bonus (1 generation)

Type: task
Status: blocked
Blocked by: 01, 02, 03

## Goal

10% от фактически начисленного бонуса **лично приглашённого** — только прямому
спонсору. Не рекурсия по дереву.

## Canon

См. `docs/03_bonus_engine.md` §3, `docs/raw/images/Description2_matching.jpeg`,
`docs/07_open_questions.md`.

## Scope

- Триггер: новые `BonusTransaction` Direct/Unilevel/(Rank, если уже есть)
- Не включать `rule_code=matching` в базу (нет matching-на-matching)
- Получатель = `sponsor_id` автора исходного бонуса
- Не от товарооборота

## Out of scope

- Matching вверх по всей цепочке
- Pool matching

## Stop

Не начинать без resolved `01` + human OK.

## Done

- [ ] Один шаг вверх, 10% из config
- [ ] Нет бесконечной рекурсии
- [ ] Идемпотентность на исходных tx

## Comments
