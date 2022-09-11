using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using open_auburn_api.Models;
using open_auburn_api.Filters;
using open_auburn_api.Helpers;
using open_auburn_api.Services;
using open_auburn_api.Wrappers;

namespace open_auburn_api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public CollegeController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter, [FromQuery] CollegeFilter cfilter)
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
            var validCFilter = new CollegeFilter(cfilter.Name, cfilter.Description, cfilter.Website, cfilter.Bulletin, cfilter.Phone);

            var filteredData = await _context.Colleges
                .Where(c => c.Name.Contains(validCFilter.Name)
                    && c.Description.Contains(validCFilter.Description)
                    && c.Website.Contains(validCFilter.Website)
                    && c.Bulletin.Contains(validCFilter.Bulletin)
                    && c.Phone.Contains(validCFilter.Phone)
                    )
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            var totalRecords = filteredData.Count;
            var pagedData = filteredData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<College>(pagedData, validPFilter, totalRecords, _uriService, newQuery);
            return Ok(pagedReponse);
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        public async Task<ActionResult<College>> GetCollege(int id)
        {
            if (_context.Colleges == null)
            {
                return NotFound();
            }
            var crime = await _context.Colleges.FindAsync(id);


            if (crime == null)
            {
                return NotFound();
            }

            return Ok(new Response<College>(crime));
        }

        private bool CollegeExists(int id)
        {
            return (_context.Colleges?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}
