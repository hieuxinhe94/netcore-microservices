using System.Threading.Tasks;
using AuthorizationService.Dal.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthorizeService.Core.Midware
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        IAuthRepository authRepository;

        public ResourceOwnerPasswordValidator(IAuthRepository _authRepository)
        {
            this.authRepository = _authRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (authRepository.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(authRepository.GetLoginInfomationByName(context.UserName).Name, "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
