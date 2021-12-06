using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Security.Claims;

namespace ContributionSystem.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public string GetUserId(ClaimsPrincipal user)
        {
            foreach (var item in user.Claims)
            {
                if (item.Type == ClaimTypes.NameIdentifier)
                {
                    return item.Value;
                }
            }
            throw new Exception("User have no id");
        }
    }
}
