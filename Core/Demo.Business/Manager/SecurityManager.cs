using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Demo.Business.Contracts;
using Demo.Data;
using Demo.Data.SecurityDomainModel;
using Demo.DomainModel;
using Demo.Shared.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demo.Business.Manager
{
    /// <summary>
    /// The SecurityManager is contains business logic regards login, user creation and application security.
    /// </summary>
    public class SecurityManager : ISecurityManager
    {
        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager; 

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManager"/> class.
        /// </summary>
        public SecurityManager()
        {
            _userManager = new UserManager<AppUser>(new UserStore<AppUser>(new DemoDataContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DemoDataContext()));
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="userRegistration">
        /// The user registration.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<DemoResponse<bool>> CreateUser(UserRegistration userRegistration)
        {
            return Task.Run(async () =>
                {
                    var response = new DemoResponse<bool>();
                    try
                    {
                        var appUser = new AppUser
                        {
                                              UserDetail = userRegistration.UserDetail,
                                              UserName = userRegistration.UserName,
                                              Email = userRegistration.UserDetail.Email,
                                              EmailConfirmed = true,
                                          };

                        // Add the basic claims like Gender and Email
                        var identityClaims = new List<IdentityUserClaim>
                                                 {
                                                     new IdentityUserClaim {ClaimType = ClaimTypes.Gender, ClaimValue = userRegistration.UserDetail.Gender == Gender.Male ? "Male" : "Female"}, 
                                                     new IdentityUserClaim { ClaimType = ClaimTypes.Email, ClaimValue = userRegistration.UserDetail.Email}, 
                                                     new IdentityUserClaim {ClaimType = ClaimTypes.Name, ClaimValue = string.Concat(userRegistration.UserDetail.FirstName, " ", userRegistration.UserDetail.LastName)}
                                                     };

                        // Add Roles claim, One User can belong to multiple roles
                        identityClaims.AddRange(userRegistration.RolesList.Select(role => new IdentityUserClaim {ClaimType = ClaimTypes.Role,ClaimValue = role}));

                        
                        // Attach all the claims to the App User
                        foreach (var identityUserClaim in identityClaims)
                        {
                            appUser.Claims.Add(identityUserClaim);
                        }
                        var isuserCreated = await _userManager.CreateAsync(appUser, userRegistration.Password);
                        if (isuserCreated.Succeeded) // user created successfully
                        {

                            var addusertoRoles = await _userManager.AddToRolesAsync(appUser.Id, userRegistration.RolesList.ToArray());
                            if (addusertoRoles.Succeeded)
                            {
                                response.Response = true;
                            }
                            else
                            {
                                foreach (var error in addusertoRoles.Errors)
                                {
                                    Log.LogException(new Exception(error));
                                }


                            }
                            //        response.Exceptions.Add("Security Error", "Error occured while creating new user");
                            
                            //foreach (var role in userRegistration.RolesList)
                            //{
                            //    var addUserToRole = await userManager.AddToRoleAsync(appUser.Id, role);
                            //    if (addUserToRole.Succeeded)
                            //    {
                            //        response.Response = true;
                            //    }
                            //    else
                            //    {
                            //        response.Response = false;
                            //        if (!addUserToRole.Errors.Any())
                            //        {
                            //            continue;
                            //        }

                            //        foreach (var error in addUserToRole.Errors)
                            //        {
                            //            Log.LogException(new Exception(error));
                            //        }

                            //        response.Exceptions.Add("Security Error", "Error occured while creating new user");
                            //    }
                            //}
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEntityValidationException)
                    {
                        Log.LogException(dbEntityValidationException);
                    }
                    return response;
                });
        }

        /// <summary>
        /// The validate user.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<DemoResponse<IIdentity>> ValidateUser(LoginModel login)
        {
            return Task.Run(async () =>
                    {
                        var response = new DemoResponse<IIdentity>();
                        try
                        {
                            var user = await _userManager.FindAsync(login.UserName, login.Password);
                            if (user != null)
                            {
                                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer); // token based
                                var claims = await AssignClaims(user);
                                identity.AddClaims(claims); 
                                response.Response = identity;
                                _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ExternalBearer);
                            }
                            else
                            {
                                response.Response = null;
                                response.Exceptions.Add("Invalid User", "User Authentication Failed");
                            }
                        }
                        catch (SecurityException securityException)
                        {
                            response.Response = null;
                            response.Exceptions.Add("SecurityException", securityException.Message);
                        }
                        catch (Exception exception)
                        {
                            response.Response = null;
                            response.Exceptions.Add("Exception", exception.Message);
                        }
                        return response;
                    });
        }

        /// <summary>
        /// The get user claims.
        /// </summary>
        /// <param name="userName">
        ///     The user identity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<DemoResponse<UserClaim>> GetUserClaims(string userName)
        {
            return Task.Run(async () =>
                {
                    var response = new DemoResponse<UserClaim>();
                    var userClaim = new UserClaim();


                    var appUser = await _userManager.FindByNameAsync(userName);
                    if (appUser !=null)
                    {
                         var claims = await AssignClaims(appUser);
                        foreach (var claim in claims)
                        {
                            switch (claim.Type)
                            {
                                case ClaimTypes.Name:
                                    userClaim.Name = claim.Value;
                                    break;

                                case ClaimTypes.Gender:
                                    userClaim.Gender = claim.Value == "1" ? Gender.Male : Gender.Female;
                                    break;

                                case ClaimTypes.Role:
                                    userClaim.RolesList.Add(claim.Value);
                                    break;

                                case ClaimTypes.Email:
                                    userClaim.Email = claim.Value;
                                    break;
                            }
                        }
                    }
                    
                        
                    

                    response.Response = userClaim;
                    return response;
                });
        }

        private async Task<IEnumerable<Claim>> AssignClaims(AppUser appUser)
        {

            var claims = await _userManager.GetClaimsAsync(appUser.Id);

            return claims;
            //var identityClaims = new List<Claim>
            //                         {
            //                             new Claim(ClaimTypes.Gender,appUser.UserDetail.Gender == Gender.Male ? "Male" : "Female"),
            //                             new Claim(ClaimTypes.Email, appUser.UserDetail.Email),
            //                             new Claim(ClaimTypes.Name,string.Concat(appUser.UserDetail.FirstName," ",appUser.UserDetail.LastName)),
            //                         };

            //// Add Roles claim, One User can belong to multiple roles
            //var rolesList = await this.userManager.GetRolesAsync(appUser.Id);

            ////var isInrole = await this.userManager.IsInRoleAsync(appUser.Id,"Customer");
            //identityClaims.AddRange(rolesList.Select(role => new Claim(ClaimTypes.Role, role)));

            //return identityClaims;
        }
    }
}
