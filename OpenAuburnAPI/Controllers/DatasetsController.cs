using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using open_auburn_api.Models;
using open_auburn_api.Filters;
using open_auburn_api.Helpers;
using open_auburn_api.Services;
using open_auburn_api.Wrappers;

#nullable enable
namespace open_auburn_api.Controllers
{
    [Route("/v1/[controller]")]
    [Route("/")]

    [ApiController]
    public class DatasetsController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public DatasetsController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter)
        {
            var route = Request.Path.Value;
            var query = Request.QueryString.ToString();
            var split = query.Split("&");
            var newQuery = route;
            if (split.Length > 0)
            {
                for (int i = 0; i < split.Length; i += 1)
                {
                    var n = split[i].ToLower();
                    if (!n.Contains("pagenumber") && !n.Contains("pagesize"))
                    {
                        newQuery += n;
                        if (i < split.Length - 1)
                        {
                            newQuery += "&";
                        }
                    }
                }
            }
            var validPFilter = new PaginationFilter(pfilter.PageNumber, pfilter.PageSize);
            var allData = await _context.Datasets.OrderByDescending(c => c.Id).ToListAsync();
            var totalRecords = allData.Count;
            var pagedData = allData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Dataset>(pagedData, validPFilter, totalRecords, _uriService, newQuery);
            return Ok(pagedReponse);
        }

        // GET: api/Dataset/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Dataset>> GetDataset(int id)
        {
            if (_context.Datasets == null)
            {
                return NotFound();
            }
            var fire = await _context.Datasets.FindAsync(id);


            if (fire == null)
            {
                return NotFound();
            }

            return Ok(new Response<Dataset>(fire));
        }

        private bool DatasetExists(int id)
        {
            return (_context.Datasets?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}