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
    public class FireController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public FireController(OpenAuburnContext context, IURIService uriService)
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
            var validFFilter = new FireFilter(ffilter.Campus, ffilter.FireDate, ffilter.MinFireDate, ffilter.MaxFireDate,
                ffilter.FireTime, ffilter.MinFireTime, ffilter.MaxFireTime,
                ffilter.DateReported, ffilter.MinDateReported, ffilter.MaxDateReported,
                ffilter.Description, ffilter.Cause, ffilter.DamageCost, ffilter.Injuries, ffilter.Deaths, ffilter.Location);

            var filteredData = await _context.Fires
                .Where(c => c.Campus.Contains(validFFilter.Campus)
                     && (String.IsNullOrEmpty(validFFilter.FireDate) ? true :
                         DateTime.Compare((DateTime)c.FireDate, DateTime.Parse(validFFilter.FireDate)) == 0)
                     && (String.IsNullOrEmpty(validFFilter.MinFireDate) ? true : 
                        DateTime.Compare((DateTime)c.FireDate, DateTime.Parse(validFFilter.MinFireDate)) >= 0)
                     && (String.IsNullOrEmpty(validFFilter.MaxFireDate) ? true : 
                        DateTime.Compare((DateTime)c.FireDate, DateTime.Parse(validFFilter.MaxFireDate)) <= 0)
                     && (String.IsNullOrEmpty(validFFilter.FireTime) ? true :
                         TimeSpan.Compare((TimeSpan)c.FireTime, TimeSpan.Parse(validFFilter.FireTime)) == 0)
                     && (String.IsNullOrEmpty(validFFilter.MinFireTime) ? true : 
                        TimeSpan.Compare((TimeSpan)c.FireTime, TimeSpan.Parse(validFFilter.MinFireTime)) >= 0)
                     && (String.IsNullOrEmpty(validFFilter.MaxFireTime) ? true : 
                        TimeSpan.Compare((TimeSpan)c.FireTime, TimeSpan.Parse(validFFilter.MaxFireTime)) <= 0)
                     && (String.IsNullOrEmpty(validFFilter.DateReported) ? true :
                         DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validFFilter.DateReported)) == 0)
                     && (String.IsNullOrEmpty(validFFilter.MinDateReported) ? true : 
                        DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validFFilter.MinDateReported)) >= 0)
                     && (String.IsNullOrEmpty(validFFilter.MaxDateReported) ? true : 
                        DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validFFilter.MaxDateReported)) <= 0)
                     && c.Description.Contains(validFFilter.Description)
                     && c.Cause.Contains(validFFilter.Cause)
                     && c.DamageCost.Contains(validFFilter.DamageCost)
                     && c.Injuries == int.Parse(validFFilter.Injuries)
                     && c.Deaths == int.Parse(validFFilter.Deaths)
                     && c.Location.Contains(validFFilter.Location))
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
