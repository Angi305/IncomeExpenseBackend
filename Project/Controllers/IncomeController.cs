using Microsoft.AspNetCore.Mvc;
using Project.Repository;
using Project.Services;

namespace Project.Controllers;

[Route("api/income")]
[ApiController]
public class IncomeController : ControllerBase
{
    private readonly IncomeRepository _repository;

    public IncomeController()
    {
        _repository = new IncomeRepository("income-data.json");
    }
    
    [HttpGet("getCategories")]
    public IActionResult GetCategories()
    {
        var category = new CategoryService();

        var result = category.GetAllIncomeCategories();
        return Ok(result);
    }
    
    [HttpPost("save")]
    public Task<IActionResult> SaveIncome([FromBody] IncomeEntryModel model)
    {
        
        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(BadRequest(ModelState));
        }

        try
        {
            
            _repository.AddEntry(model);

            Console.WriteLine($"Received income data: {Newtonsoft.Json.JsonConvert.SerializeObject(model)}");

            return Task.FromResult<IActionResult>(Ok(new { message = "Income data saved successfully" }));
        }
        catch (Exception ex)
        {
            return Task.FromResult<IActionResult>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    // GET: api/income
    [HttpGet]
    public ActionResult<IEnumerable<IncomeEntryModel>> GetAllEntries()
    {
        return _repository.GetAllEntries();
    }

    // GET: api/income/{id}
    [HttpGet("{id}")]
    public ActionResult<IncomeEntryModel> GetEntry(string id)
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
    public ActionResult<IncomeEntryModel> AddEntry(IncomeEntryModel entry)
    {
        _repository.AddEntry(entry);
        return CreatedAtAction(nameof(GetEntry), new { id = entry.Id }, entry);
    }

    // PUT: api/income/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateEntry(string id, IncomeEntryModel updatedEntry)
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
    public IActionResult DeleteEntry(string id)
    {
        var entry = _repository.GetEntry(id);
        if (entry == null)
        {
            return NotFound();
        }

        _repository.DeleteEntry(id);
        return NoContent();
    }
}