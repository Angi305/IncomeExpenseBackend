using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Repository;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/month")]
    public class MonthController : ControllerBase
    {

        public MonthController()
        {
        }

        [HttpGet("AutocompleteForMonths")]
        public IActionResult AutocompleteForMonths([FromQuery] string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest(new { search = "The search field is required." });
            }

            var matchingMonths = Enum.GetNames(typeof(Months))
                .Where(m => m.ToLower().Contains(search.ToLower()))
                .ToList();

            return Ok(matchingMonths);
        }
        
    }
}