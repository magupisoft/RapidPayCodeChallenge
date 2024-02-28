using RapidPay.Domain.Requests;

namespace RapidPay.CardManagement;

public interface IUserSecurityService
{
    public Task<(string?, DateTime?)> GetAccessToken(UserLoginRequest request);

}
