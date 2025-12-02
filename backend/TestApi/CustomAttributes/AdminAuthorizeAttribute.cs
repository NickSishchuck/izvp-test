using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TestApi.Filters;

namespace TestApi.CustomAttributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAuthorizeAttribute : ServiceFilterAttribute
    {
        public AdminAuthorizeAttribute() : base(typeof(AdminAuthorizationFilter))
        {
        }
    }
}
