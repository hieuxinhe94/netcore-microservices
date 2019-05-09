using AuthorizationService.Dal.Interfaces;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeService.Core.Midware
{
    public class LoginProfileService: IProfileService
    {
        private IAuthRepository _authRepository;

        public LoginProfileService(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = _authRepository.GetLogInInfomationById(int.Parse(subjectId));

                var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
				//add as many claims as you want!new Claim(JwtClaimTypes.Email, user.Email),new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
			};

                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception x)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var loginInfo = _authRepository.GetLogInInfomationById(int.Parse(context.Subject.GetSubjectId()));
            context.IsActive = (loginInfo != null) && loginInfo.Active;
            return Task.FromResult(0);
        }
    }
}
