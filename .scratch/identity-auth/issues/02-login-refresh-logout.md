# Login, refresh, logout

Type: task
Status: ready
Blocked by:

## Goal

`POST /api/v1/auth/login|refresh|logout`: JWT access + refresh в httpOnly cookie,
сессия в `AuthSession`.

## Canon

См. `docs/api-contracts/endpoints.md`, `docs/06_security.md`.
`JwtOptions`, `AuthSession`, `DeviceIdCookieMiddleware` уже в проекте.

## Scope

- Login: проверка пароля, lockout по `FailedLoginCount` / `LockoutEnd`
- Access JWT (короткий TTL) + refresh hash в `AuthSession`
- Refresh ротация; logout ревокает сессию (`RevokedAt`)
- Cookie httpOnly; не возвращать raw refresh в JSON body (или только dev-исключение — не делать)
- Не логировать email/phone/token (Serilog filter в `Program.cs` уже есть)

## Out of scope

- 2FA
- OAuth / внешние провайдеры

## Done

- [ ] Login неверного пароля не утекает, существует ли email
- [ ] Refresh после logout не проходит
- [ ] CORS + cookie работают с origin из `WebCorsOptions`

## Comments
