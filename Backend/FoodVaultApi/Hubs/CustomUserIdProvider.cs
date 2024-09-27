using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace FoodVaultApi.Hubs
{
    [Authorize]
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var headers = connection.GetHttpContext()?.Request.Headers;
            if (headers != null && headers.TryGetValue("Authorization", out var headerAuth))
            {
                var jwtToken = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
                var userIdClaim = jsonToken.Claims.First(x => x.Type == "UserId");
                return userIdClaim.Value;
            }
            return string.Empty;
        }
    }
}
