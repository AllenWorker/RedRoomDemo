namespace RedRoomDemo.Models;

public class OrderDetailsViewModel
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string? CustomerEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = string.Empty;

    public bool HasPaymentMatch { get; set; }
    public string PaymentMatchNote { get; set; } = string.Empty;
    public List<PaymentTransaction> MatchedPayments { get; set; } = [];
}
