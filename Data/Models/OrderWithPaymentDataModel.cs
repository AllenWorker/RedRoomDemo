namespace RedRoomDemo.Data.Models;

public class OrderWithPaymentDataModel
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string? PaymentStatus { get; set; }
    public int HasMatchedPaymentInt { get; set; }
}
