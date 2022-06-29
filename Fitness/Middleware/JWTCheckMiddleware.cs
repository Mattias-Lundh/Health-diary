
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Linq;
using Fitness.Authorization;

namespace Fitness.Middleware
{
    public class JWTCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private string[] _unprotectedPaths;
        private AppUserAuthentication _appUserAuthentication;

        public JWTCheckMiddleware(RequestDelegate next, AppUserAuthentication appUserAuthentication)
        {
            _next = next;
            _unprotectedPaths = new[] { "/api/appuser/login", "/api/appuser/register" };
            _appUserAuthentication = appUserAuthentication;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_unprotectedPaths.Contains(context.Request.Path.Value.ToLower()))
            {
                await _next(context);
            }
            else if (context.Request.Method == "OPTIONS")
            {
                // allow CORS pre flight request
                await _next(context);
            }
            else
            {
                var jwt = context.Request.Headers["Authorization"];
                Context.UserId = _appUserAuthentication.ValidateToken(jwt.First());
                await _next(context);
            }            
        }
    }
}
