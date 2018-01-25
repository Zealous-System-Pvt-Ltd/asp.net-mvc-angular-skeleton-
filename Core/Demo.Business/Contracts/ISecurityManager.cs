using System.Security.Principal;
using System.Threading.Tasks;
using Demo.Data.SecurityDomainModel;
using Demo.Shared.Helpers;

namespace Demo.Business.Contracts
{
    /// <summary>
    /// The Security Manager interface.
    /// </summary>
    public interface ISecurityManager
    {
        /// <summary>
        /// register user for login
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        Task<DemoResponse<bool>> CreateUser(UserRegistration userRegistration);

        /// <summary>
        /// validate user credentials
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<DemoResponse<IIdentity>> ValidateUser(LoginModel login);

        /// <summary>
        /// get user claims by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<DemoResponse<UserClaim>> GetUserClaims(string userName);

    }
}
