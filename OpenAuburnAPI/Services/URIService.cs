using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using open_auburn_api.Filters;
using open_auburn_api.Models;
using open_auburn_api.Services;
using System.Configuration;

namespace open_auburn_api.Services
{
    public class URIService : IURIService
    {
        private readonly string _uri;
        public URIService(string uri)
        {
            _uri = uri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_uri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OpenAuburnContext>(options =>
                options.UseSqlServer("",
                    b => b.MigrationsAssembly(typeof(OpenAuburnContext).Assembly.FullName)));
            services.AddHttpContextAccessor();
            services.AddSingleton<IURIService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new URIService(uri);
            });
            services.AddControllers();
        }
    }
}
