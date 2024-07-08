namespace Project.Controllers;

public class ExpenseEntryModel
{
    public string Id { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string ValueType { get; set; }
}