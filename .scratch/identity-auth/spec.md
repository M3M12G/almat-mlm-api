# Identity / Auth

См. `docs/api-contracts/endpoints.md` (Auth, `GET /me`), `docs/00_overview.md`,
`docs/06_security.md`. JWT options: `JwtOptions` (`Issuer` / `Audience` /
`SigningKey` / TTL). Сущности уже есть: `User`, `AuthSession`.

Контроллер сейчас stub (`IdentityController` Ping). Маршруты — **по контракту**
`/api/v1/auth/*` и `/api/v1/me`, не `api/Identity`.

## Issues

| # | File | Status |
|---|---|---|
| 01 | [register-with-referral](issues/01-register-with-referral.md) | ready |
| 02 | [login-refresh-logout](issues/02-login-refresh-logout.md) | ready |
| 03 | [me-profile](issues/03-me-profile.md) | ready |

## Out of scope

- `POST /auth/verify-phone` (отдельный слайс)
- 2FA
- Смена спонсора после регистрации (см. `admin-users`)
