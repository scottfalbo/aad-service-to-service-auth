using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AadAuth.Handlers
{
    public class ScryForRequirements : AuthorizationHandler<Requirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirements requirements)
        {
            if (context == null)
                throw new ArgumentException();

            if (requirements == null)
                throw new ArgumentException();

            var roleClaims = context.User.Claims.Where(x => x.Type == "roles");

            if (roleClaims != null && ScryForRoles(roleClaims))
                context.Succeed(requirements);

            return Task.CompletedTask;
        }

        private static bool ScryForRoles(IEnumerable<Claim> roleClaims)
        {
            foreach (var role in roleClaims)
            {
                if (role.Value == "wizard")
                    return true;
            }
            return false;
        }
    }
}
