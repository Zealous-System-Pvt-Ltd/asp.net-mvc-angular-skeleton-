// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="zealous">
//   MIT License
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Demo.Business;
using Demo.Web.AuthProviders;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(Demo.Web.Startup))]

namespace Demo.Web
{
    public class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            UnityConfig.RegisterComponents(config);
            AutoMapperConfig.RegisterMappings();
        }

        /// <summary>
        /// The OAuth Configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            var authServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(5),  // this should be generally in form of minutes
                Provider = new BasicAuthProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}