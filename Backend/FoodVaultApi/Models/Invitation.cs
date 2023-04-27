namespace FoodVaultApi.Models
{
    public class Invitation
    {
        public string Id { get; set; }
        public string SentFrom { get; set; }
        public string SentTo { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public bool Accepted { get; set; } = false;
        public string GroupId { get; set; }

        public User SentFromUser { get; set; } // The user who sent the invitation
        public User SentToUser { get; set; } // The user receiving the invitation
        public Group Group { get; set; } // the group the user is being invited to join
    }
}
