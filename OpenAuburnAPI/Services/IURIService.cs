using open_auburn_api.Filters;

namespace open_auburn_api.Services
{
    public interface IURIService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
