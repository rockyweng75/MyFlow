using MyFlow.Domain.Models;
using JWT;

namespace MyFlow.WebApi.Security
{
    public static class AuthorizeControls
    {
        public static Dictionary<string, object> CreatePayload(UserInfoVM userInfo)
        {
            var expireMinutes = 90;
            var provider = new UtcDateTimeProvider();
            var now = provider.GetNow();

            var secondsSinceEpoch = UnixEpoch.GetSecondsSince(now) + (expireMinutes * 60);

            Dictionary<string, object> payload;

            payload = new Dictionary<string, object>
            {
                { "sub", userInfo.UserId! },
                { "exp", secondsSinceEpoch },
                { CustomClaimTypes.UserName, userInfo.UserName! },
                { CustomClaimTypes.DeptCode, userInfo.DeptCode! },
                { CustomClaimTypes.DeptName, userInfo.DeptName! },
            };

            payload.Add("role", string.Join(',', userInfo.Roles));

            return payload;
        }
    }
}
