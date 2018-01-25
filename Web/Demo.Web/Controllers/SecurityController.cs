using Demo.Business.Contracts;
using Demo.Data.SecurityDomainModel;
using Demo.Shared.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.Web.Controllers
{
    /// <summary>
    /// The security controller.
    /// </summary>
    [RoutePrefix("security")]
    public class SecurityController : ApiController
    {
        /// <summary>
        /// The security manager.
        /// </summary>
        private readonly ISecurityManager _securityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        /// <param name="securityManager">
        /// The security manager.
        /// </param>
        public SecurityController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        /// <summary>
        /// The register user.
        /// </summary>
        /// <param name="userRegistration">
        /// The user registration.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> RegisterUser([FromBody] UserRegistration userRegistration)
        {
            var result = await _securityManager.CreateUser(userRegistration);
            if (!result.Exceptions.Any())
            {
                return Created("security/register", userRegistration);
            }

            return InternalServerError(new Exception(result.Exceptions.DictionaryToString()));
        }

        /// <summary>
        /// The get user claims.
        /// </summary>
        /// <param name="userName">
        /// The user Name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>
        /// </returns>
        [HttpGet]
        [Route("claims")]
        public async Task<IHttpActionResult> GetUserClaims([FromUri]string userName)
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            if (principal == null)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            var result = await _securityManager.GetUserClaims(userName);
            if (!result.Exceptions.Any())
            {
                return Ok(result.Response);
            }

            return InternalServerError(new Exception(result.Exceptions.DictionaryToString()));
        }
    }
}