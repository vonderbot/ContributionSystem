using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace ContributionSystem.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public string GetUserId(ClaimsPrincipal user)
        {
            var Value = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(Value))
            {
                throw new Exception("User have no id");
            }
            else
            {
                return Value;
            }
        }
    }
}
