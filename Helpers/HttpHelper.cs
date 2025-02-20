using SmartScheduler.Constants;
using SmartScheduler.Data.DataTransferObjects;
using SmartScheduler.Data.Models;
using System;
using System.Security.Claims;

namespace SmartScheduler.Helpers
{
    public static class HttpHelper
    {
        public static TokenUserData GetUserDataFromToken(HttpRequest request)
        {
            Claim? id = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenConstants.Id);
            Claim? userName = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenConstants.Name);
            Claim? role = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenConstants.Role);

            if(id == null || userName == null || role == null)
            {
                throw new Exception("Invalid authorization token, necessary claims missing");
            }

            _ = Enum.TryParse<UserRole>(role.Value, out UserRole userRole);

            return new()
            {
                Id = int.Parse(id.Value),
                UserName = userName.Value,
                Role = userRole, // TODO: Fix this
            };
        }
    }
}
