namespace FoodVaultApi.DTO
{
    public class InvitationDTO
    {
    }

    public class InvitationPost
    {
        public string fromUserId { get; set; }
        public string toUserId { get; set; }
        public string groupId { get; set; }
    }
}
