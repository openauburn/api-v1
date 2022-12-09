using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using open_auburn_api.Models;
using open_auburn_api.Filters;
using open_auburn_api.Helpers;
using open_auburn_api.Services;
using open_auburn_api.Wrappers;
using static System.Net.Mime.MediaTypeNames;

#nullable enable
namespace open_auburn_api.Controllers
{
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class Root : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public Root(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDatasets([FromQuery] PaginationFilter pfilter)
        {
            var route = Request.Path.Value;
            var query = PaginationHelper.IsolateFilterQuery(Request.QueryString.ToString());

            var validPFilter = new PaginationFilter(pfilter.PageNumber, pfilter.PageSize);
            var allData = await _context.Datasets.OrderByDescending(c => c.Id).ToListAsync();
            var totalRecords = allData.Count;
            var pagedData = allData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Dataset>(pagedData, validPFilter, totalRecords, _uriService, route, query);
            return Ok(pagedReponse);
        }

        [HttpGet("favicon.ico")]
        public IActionResult GetFavicon()
        {
            return PhysicalFile(@"C:\favicon.ico", "image/x-icon");
        }


        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}