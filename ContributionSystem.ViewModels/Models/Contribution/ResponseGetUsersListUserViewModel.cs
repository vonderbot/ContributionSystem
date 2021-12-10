using ContributionSystem.ViewModels.Common;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetUsersListUserViewModel : CollectionOfItems<ResponseGetUsersListContributionViewModelItem>
    {
    }

    public class ResponseGetUsersListContributionViewModelItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Status { get; set; }
    }
}
