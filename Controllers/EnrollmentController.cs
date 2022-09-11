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
    public class EnrollmentController : ControllerBase
    {
        private readonly OpenAuburnContext _context;
        private readonly IURIService _uriService;

        public EnrollmentController(OpenAuburnContext context, IURIService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSome([FromQuery] PaginationFilter pfilter, [FromQuery] EnrollmentFilter efilter)
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
            var validEFilter = new EnrollmentFilter(efilter.Major, efilter.MajorCode, efilter.College, efilter.Term, efilter.UGMale, efilter.UGFemale, efilter.GRMale, efilter.GRFemale, efilter.Total);

            var filteredData = await _context.Enrollments
                .Where(e => e.Major.Contains(validEFilter.Major)
                     && e.MajorCode.Contains(validEFilter.MajorCode)
                     && e.College.Contains(validEFilter.College)
                     && e.Term.Contains(validEFilter.Term)
                     && (String.IsNullOrEmpty(validEFilter.UGMale) ? true :
                        e.UGMale == int.Parse(validEFilter.UGMale))
                    && (String.IsNullOrEmpty(validEFilter.UGFemale) ? true :
                        e.UGFemale == int.Parse(validEFilter.UGFemale))
                    && (String.IsNullOrEmpty(validEFilter.GRMale) ? true :
                        e.GRMale == int.Parse(validEFilter.GRMale))
                    && (String.IsNullOrEmpty(validEFilter.GRFemale) ? true :
                        e.GRFemale == int.Parse(validEFilter.GRFemale))
                    && (String.IsNullOrEmpty(validEFilter.Total) ? true :
                        e.Total == int.Parse(validEFilter.Total)))
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            var totalRecords = filteredData.Count;
            var pagedData = filteredData
                .Skip((validPFilter.PageNumber - 1) * validPFilter.PageSize)
                .Take(validPFilter.PageSize)
                .ToList();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Enrollment>(pagedData, validPFilter, totalRecords, _uriService, newQuery);
            return Ok(pagedReponse);
        }

        // GET: api/Enrollment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            if (_context.Enrollments == null)
            {
                return NotFound();
            }
            var crime = await _context.Enrollments.FindAsync(id);


            if (crime == null)
            {
                return NotFound();
            }

            return Ok(new Response<Enrollment>(crime));
        }

        private bool EnrollmentExists(int id)
        {
            return (_context.Enrollments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("Error")]
        public IActionResult GetError() =>
            Problem("Something went wrong.");
    }
}
