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
        public Uri GetPageUri(PaginationFilter filter, string route, string query)
        {
            var _enpointUri = new Uri(string.Concat(_uri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "page_number", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "page_size", filter.PageSize.ToString());
            if(query != String.Empty)
            {
                modifiedUri += "&" + query;
            }

            return new Uri(modifiedUri);
        }
    }
}
