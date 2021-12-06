using System.Security.Claims;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(ClaimsPrincipal user);
    }
}
