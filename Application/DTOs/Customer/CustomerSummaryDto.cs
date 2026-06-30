namespace RedRoomDemo.Application.DTOs.Customer;

public class CustomerSummaryDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OrderCount { get; set; }
}
