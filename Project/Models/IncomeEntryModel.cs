namespace Project.Controllers;

public class IncomeEntryModel
{
    public string Id { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public string Category { get; set; }
    public decimal Salary { get; set; }
    public decimal Amount { get; set; }
    public string? ValueType { get; set; }
}