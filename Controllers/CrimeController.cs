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
    public class CrimeController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public CrimeController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter, [FromQuery] CrimeFilter cfilter)
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
            Console.WriteLine(route);
            Console.WriteLine(query);
            var validPFilter = new PaginationFilter(pfilter.PageNumber, pfilter.PageSize);
            var validCFilter = new CrimeFilter(cfilter.Campus, cfilter.Date, cfilter.MinDate, cfilter.MaxDate, cfilter.IncidentType, cfilter.CleryClass, cfilter.Location);

            var filteredData = await _context.Crimes
                .Where(c => c.Campus.Contains(validCFilter.Campus)
                     && (String.IsNullOrEmpty(validCFilter.Date) ? true :
                         DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validCFilter.Date)) == 0)
                     && DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validCFilter.MinDate)) >= 0
                     && DateTime.Compare((DateTime)c.DateReported, DateTime.Parse(validCFilter.MaxDate)) <= 0
                     && c.IncidentType.Contains(validCFilter.IncidentType)
                     && c.CleryClass.Contains(validCFilter.CleryClass)
                     && c.Location.Contains(validCFilter.Location))
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            var totalRecords = filteredData.Count;
            var pagedData = filteredData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Crime>(pagedData, validPFilter, totalRecords, _uriService, newQuery);
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

        // PUT: api/Crime/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrime(int id, Crime crime)
        {
            if (id != crime.Id)
            {
                return BadRequest();
            }

            _context.Entry(crime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Crime
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Crime>> PostCrime(Crime crime)
        {
            if (_context.Crimes == null)
            {
                return Problem("Entity set 'OpenAuburnContext.Crimes'  is null.");
            }
            _context.Crimes.Add(crime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrime", new { id = crime.Id }, crime);
        }

        // DELETE: api/Crime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrime(int id)
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

            _context.Crimes.Remove(crime);
            await _context.SaveChangesAsync();

            return NoContent();
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
