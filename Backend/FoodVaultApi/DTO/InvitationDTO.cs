namespace FoodVaultApi.DTO
{
    public class InvitationDTO
    {
        public string invitationId { get; set; }
        public string fromUserFullName { get; set; } // the firstname + lastname of the user who invited the user
        public string fromUserUsername { get; set; } // the username of the user who invited the user
        public string groupId { get; set; } // the id of the group invited to
        public string groupName { get; set; } // the name of the group user is invited to
        public DateTime sent { get; set; } // when the invite was sent

        public static InvitationDTO ToDTO(string id, string sentFromName, string sentFromUsername, string groupId, string groupName, DateTime sent)
        {
            return new InvitationDTO
            {
                invitationId = id,
                fromUserFullName = sentFromName,
                fromUserUsername = sentFromUsername,
                groupId = groupId,
                groupName = groupName,
                sent = sent
            };
        }
    }

    public class InvitationPost
    {
        public string fromUserId { get; set; }
        public string toUserId { get; set; }
        public string groupId { get; set; }
    }
}
