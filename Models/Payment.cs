namespace Project.Controllers;

public class Payment
{
    public int Id { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public string Category { get; set; }
    public decimal Food { get; set; }
    public decimal Transportation { get; set; }
    public decimal Bills { get; set; }
    public decimal Other { get; set; }
}
