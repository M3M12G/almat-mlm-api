# .scratch — backend tickets

One feature per folder: `.scratch/<feature-slug>/spec.md`.

**Link, don't copy** canon from the `docs/` submodule:

```markdown
См. `docs/02_network_model.md` и `docs/api-contracts/endpoints.md`.
```

See `docs/agents/issue-tracker.md`.

## Features (pilot handoff)

| Folder | Status | Notes |
|---|---|---|
| [identity-auth](identity-auth/spec.md) | ready | register / JWT / `GET /me` |
| [network](network/spec.md) | ready | cycles, ancestors, tree |
| [catalog-orders](catalog-orders/spec.md) | ready | packages + create order |
| [payments](payments/spec.md) | ready | FreedomPay webhook, **no** bonus engine |
| [wallet](wallet/spec.md) | ready | withdrawal request + admin queue |
| [admin-users](admin-users/spec.md) | ready | list / guarded sponsor patch |
| [bonus-rules](bonus-rules/spec.md) | ready | seed `config_json` only |
| [bonus-engine](bonus-engine/spec.md) | blocked | wait `01` freeze assumptions + human OK |
| [ranks](ranks/spec.md) | blocked | RankEngine after assumptions |
| [leadership-pool](leadership-pool/spec.md) | blocked | monthly job after ranks |

Status on issue files: `ready` \| `blocked` \| `claimed` \| `resolved`.
