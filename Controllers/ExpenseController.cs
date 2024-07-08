using Microsoft.AspNetCore.Mvc;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/expense")] 
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseRepository _repository;

        public ExpenseController()
        {
            _repository = new ExpenseRepository("expanse-data.json"); 
        }
        
        [HttpPost("save")]
        public async Task<IActionResult> SaveExpense([FromBody] ExpenseEntryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.AddEntry(model);

                Console.WriteLine($"Received income data: {Newtonsoft.Json.JsonConvert.SerializeObject(model)}");

                return Ok(new { message = "Income data saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/income
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseEntryModel>> GetAllEntries()
        {
            return _repository.GetAllEntries(); 
        }

        // GET: api/income/{id}
        [HttpGet("{id}")]
        public ActionResult<ExpenseEntryModel> GetEntry(string id)
        {
            var entry = _repository.GetEntry(id); 
            if (entry == null)
            {
                return NotFound(); 
            }
            return entry; 
        }

        // POST: api/income
        [HttpPost]
        public ActionResult<ExpenseEntryModel> AddEntry(ExpenseEntryModel entry)
        {
            _repository.AddEntry(entry); 
            return CreatedAtAction(nameof(GetEntry), new { id = entry.Id }, entry); 
        }

        // PUT: api/income/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEntry(string id, ExpenseEntryModel updatedEntry)
        {
            if (id != updatedEntry.Id)
            {
                return BadRequest(); 
            }
            _repository.UpdateEntry(updatedEntry); 
            return NoContent();
        }

        // DELETE: api/income/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(string id)
        {
            var entry = _repository.GetEntry(id); 
            if (entry == null)
            {
                return NotFound(); 
            }

            _repository.DeleteExpense(id);
            return NoContent(); 
        }
        
        [HttpGet("getCategories")]
        public IActionResult GetCategories()
        {
            var category = new CategoryService();

            var result = category.GetAllExpenseCategories();
            return Ok(result);
        }
    }
}
