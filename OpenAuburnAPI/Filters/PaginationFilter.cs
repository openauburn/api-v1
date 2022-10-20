using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace open_auburn_api.Filters
{
    public class PaginationFilter
    {
        [FromQuery(Name = "page_number")]
        public int PageNumber { get; set; }
        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 50;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 50 ? 50 : pageSize;
        }
    }
}
