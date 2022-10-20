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
    [ApiController]
    public class CrimesController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public CrimesController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter, [FromQuery] CrimeFilter cfilter)
        {
            var route = Request.Path.Value;
            var query = PaginationHelper.IsolateFilterQuery(Request.QueryString.ToString());

            var validPFilter = new PaginationFilter(pfilter.PageNumber, pfilter.PageSize);
            var validCFilter = new CrimeFilter(cfilter.Campus, cfilter.Date, cfilter.MinDate, cfilter.MaxDate, cfilter.IncidentType, cfilter.CleryClass, cfilter.Address);

            var filteredData = await _context.Crimes
                .Where(c => c.Campus.Contains(validCFilter.Campus)
                     && (String.IsNullOrEmpty(validCFilter.Date) ? true :
                         DateTime.Compare((DateTime)c.ReportedOn, DateTime.Parse(validCFilter.Date)) == 0)
                     && DateTime.Compare((DateTime)c.ReportedOn, DateTime.Parse(validCFilter.MinDate)) >= 0
                     && DateTime.Compare((DateTime)c.ReportedOn, DateTime.Parse(validCFilter.MaxDate)) <= 0
                     && c.IncidentType.Contains(validCFilter.IncidentType)
                     && c.CleryClass.Contains(validCFilter.CleryClass)
                     && c.Address.Contains(validCFilter.Address))
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            var totalRecords = filteredData.Count;
            var pagedData = filteredData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Crime>(pagedData, validPFilter, totalRecords, _uriService, route, query);
            return Ok(pagedReponse);
        }

        // GET: api/Crime/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crime>> GetCrime(int id)
        {
            if (_context.Crimes == null)
            {
                return NotFound();
            }
            var crime = await _context.Crimes.FindAsync(id);


            if (crime == null)
            {
                return NotFound();
            }

            return Ok(new Response<Crime>(crime));
        }

        private bool CrimeExists(int id)
        {
            return (_context.Crimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}
