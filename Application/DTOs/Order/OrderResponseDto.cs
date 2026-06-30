namespace RedRoomDemo.Application.DTOs.Order;

public class OrderResponseDto
{
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool HasMatchedPayment { get; set; }
}
