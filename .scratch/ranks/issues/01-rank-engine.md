# Rank engine + one-time bonus

Type: task
Status: blocked
Blocked by: bonus-engine/01

## Goal

После оплаченной покупки проверить ранговые условия (2 ЛП нужного ранга;
Консультант = 2 ЛП + 50 продаж в их командах), выдать ранг и разовую премию
один раз.

## Canon

См. `docs/03_bonus_engine.md` §4, `docs/raw/images/Description5_Career.jpeg`,
`Rank` / `RankAchievement`. Условия — в `required_condition_json`, не в switch.

## Scope

- Seed JSON условий для 11 рангов (сейчас `{}`)
- 2 ЛП = personal frontline с `RankId` ≥ требуемого (или exact — выбрать по JSON)
- Премия в `bonus_transactions`; `RankAchievement.BonusPaid = true`
- Ранг не снимать (pin, допущение `01`)
- Не down-scan всей компании на лету: использовать агрегаты / ограниченный
  подсчёт первой линии (см. `02_network_model.md`)

## Out of scope

- Leadership Pool
- Демоушен / monthly requalification

## Stop

Не начинать без resolved `bonus-engine/01` + human OK (`AGENTS.md` деньги).

## Done

- [ ] Повторная проверка не платит премию дважды
- [ ] Условия из JSON
- [ ] Тест: 2 ЛП-Консультанта → Старший консультант; 2 консультанта вглубине
      без ЛП — не проходит

## Comments
