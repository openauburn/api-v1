using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using open_auburn_api.Models;
using open_auburn_api.Filters;
using open_auburn_api.Helpers;
using open_auburn_api.Services;
using open_auburn_api.Wrappers;

namespace open_auburn_api.Controllers
{
    [Route("/v1/[controller]")]
    [ApiController]
    public class FiresController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public FiresController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter, [FromQuery] FireFilter ffilter)
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
            var validFFilter = new FireFilter(ffilter.Campus, ffilter.OccurredAt, ffilter.ReportedOn,
                ffilter.Description, ffilter.Cause, ffilter.DamageCost, ffilter.Injuries, ffilter.Deaths, ffilter.Address);

            var filteredData = await _context.Fires
                .Where(c => c.Campus.Contains(validFFilter.Campus)
                     && (String.IsNullOrEmpty(validFFilter.OccurredAt) ? true :
                         DateTime.Compare((DateTime)c.OccurredAt, DateTime.Parse(validFFilter.OccurredAt)) == 0)
                     && (String.IsNullOrEmpty(validFFilter.ReportedOn) ? true :
                         DateTime.Compare((DateTime)c.ReportedOn, DateTime.Parse(validFFilter.ReportedOn)) == 0)
                     && c.Description.Contains(validFFilter.Description)
                     && c.Cause.Contains(validFFilter.Cause)
                     && c.DamageCost.Contains(validFFilter.DamageCost)
                     && c.Injuries == int.Parse(validFFilter.Injuries)
                     && c.Deaths == int.Parse(validFFilter.Deaths)
                     && c.Address.Contains(validFFilter.Address))
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            var totalRecords = filteredData.Count;
            var pagedData = filteredData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Fire>(pagedData, validPFilter, totalRecords, _uriService, newQuery);
            return Ok(pagedReponse);
        }

        // GET: api/Fire/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Fire>> GetFire(int id)
        {
            if (_context.Fires == null)
            {
                return NotFound();
            }
            var fire = await _context.Fires.FindAsync(id);


            if (fire == null)
            {
                return NotFound();
            }

            return Ok(new Response<Fire>(fire));
        }

        private bool FireExists(int id)
        {
            return (_context.Fires?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}