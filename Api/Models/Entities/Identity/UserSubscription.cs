using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Identity
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public string SubscriberId { get; set; }
        public string TargetUserId { get; set; }

        [ForeignKey(nameof(SubscriberId))]
        public ApplicationUser Subscriber { get; set; }
        [ForeignKey(nameof(TargetUserId))]
        public ApplicationUser TargetUser { get; set; }
    }
}
