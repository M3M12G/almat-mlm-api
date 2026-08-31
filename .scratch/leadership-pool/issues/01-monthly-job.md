# Leadership Pool monthly job

Type: task
Status: blocked
Blocked by: bonus-engine/01, ranks/01

## Goal

Заменить stub `LeadershipPoolJob`: 2% мирового ТО за месяц → баллы рангов
Gold Director+ → `bonus_transactions` + `PoolPeriod` / `PoolDistribution`.

## Canon

См. `docs/03_bonus_engine.md` §5, `docs/raw/images/Description4_LeadershipPool.jpeg`,
ADR-0004. Cron уже настроен (1-е число, Asia/Almaty).

## Scope

- Мировой ТО = сумма `purchases` status paid за период (не ветка пользователя)
- Баллы из `Rank.LeadershipPoolPoints` (seed уже 1/3/7/15)
- Участники: текущий `RankId` на конец периода (допущение `01`)
- Идемпотентность периода: повторный fire не двоит (DisallowConcurrent + unique period)
- Admin recalculate — не в этом тикете (endpoint в контракте, отдельный follow-up)

## Out of scope

- Бывшие ранги / демоушен
- Dashboard Quartz (уже есть)

## Stop

Не начинать без `01` assumptions + RankEngine + human OK.

## Done

- [ ] Пример из слайда воспроизводится тестом (фонд / баллы / 1 балл)
- [ ] Stub-лог заменён реальным расчётом
- [ ] Повтор job за тот же period безопасен

## Comments
