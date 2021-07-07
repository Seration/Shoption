using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace Shoption.IdentityServer.Services
{
    public class TokenExchangeExtentionGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _tokenValidator;

        public TokenExchangeExtentionGrantValidator(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public string GrantType => "urn:ieft:params:oauth:grant-type:token-exchange";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var requestRaw = context.Request.Raw.ToString();

            var token = context.Request.Raw.Get("subject_token");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest, "Token missing");
                return;
            }

            var tokenValidateResult = await _tokenValidator.ValidateAccessTokenAsync(token);

            if (tokenValidateResult.IsError)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "Token invalid");
                return;
            }

            var subjectClaim = tokenValidateResult.Claims.FirstOrDefault(x => x.Type == "sub");

            if (subjectClaim == null)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "token must contain sub value");
                return;
            }

            context.Result = new GrantValidationResult(subjectClaim.Value, "access_token", tokenValidateResult.Claims);
            return;
        }
    }
}
