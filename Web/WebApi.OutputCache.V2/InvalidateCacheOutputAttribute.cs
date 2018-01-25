using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.OutputCache.V2
{
    using Microsoft.Practices.ServiceLocation;

    using WebApi.OutputCache.Core.Cache;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class InvalidateCacheOutputAttribute : BaseCacheAttribute
    {
        private string _controller;
        private readonly string _methodName;

        public InvalidateCacheOutputAttribute(string methodName)
            : this(methodName, null)
        {
        }

        public InvalidateCacheOutputAttribute(string methodName, Type type = null)
        {
            _controller = type != null ? type.Name.Replace("Controller", string.Empty) : null;
            _methodName = methodName;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null && !actionExecutedContext.Response.IsSuccessStatusCode) return;
            _controller = _controller ?? actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

            var config = actionExecutedContext.Request.GetConfiguration();
            //EnsureCache(config, actionExecutedContext.Request);
            var apiCache = ServiceLocator.Current.GetInstance<IApiOutputCache>();
            var key = actionExecutedContext.Request.GetConfiguration().CacheOutputConfiguration().MakeBaseCachekey(_controller, _methodName);
            if (apiCache.Contains(key))
            {
                apiCache.RemoveStartsWith(key);
            }
        }
    }
}