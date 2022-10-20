using open_auburn_api.Filters;
using open_auburn_api.Services;
using open_auburn_api.Wrappers;
using System.IO.IsolatedStorage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace open_auburn_api.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter pageFilter, int totalRecords, IURIService uriService, string route, string query)
        {
            var respose = new PagedResponse<List<T>>(pagedData, pageFilter.PageNumber, pageFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)pageFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                pageFilter.PageNumber >= 1 && pageFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(pageFilter.PageNumber + 1, pageFilter.PageSize), route, query)
                : null;
            respose.PreviousPage =
                pageFilter.PageNumber - 1 >= 1 && pageFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(pageFilter.PageNumber - 1, pageFilter.PageSize), route, query)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, pageFilter.PageSize), route, query);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, pageFilter.PageSize), route, query);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static string IsolateFilterQuery(string query)
        {
            var onlyFilter = string.Empty;
            var querySplit = query.Split("?");
            Console.WriteLine(querySplit[1]);
            if (querySplit.Length > 1)
            {
                var split = querySplit[1].Split("&");

                if (split.Length > 0)
                {
                    for (int i = 0; i < split.Length; i += 1)
                    {
                        var n = split[i].ToLower();
                        if (!n.Contains("page_number") && !n.Contains("page_size"))
                        {
                            onlyFilter += n;
                            if (i < split.Length - 1)
                            {
                                onlyFilter += "&";
                            }
                        }
                    }
                }
            }
            return onlyFilter;
        }
    }
}
