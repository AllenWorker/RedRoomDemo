namespace RedRoomDemo.Models;

public class CustomerListItemViewModel
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalOrderAmount { get; set; }
}
