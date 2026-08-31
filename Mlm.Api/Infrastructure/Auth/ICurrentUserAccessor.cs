namespace Mlm.Api.Infrastructure.Auth;

internal interface ICurrentUserAccessor
{
    Guid? UserId { get; }
}
