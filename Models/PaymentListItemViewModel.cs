namespace RedRoomDemo.Models;

public class PaymentListItemViewModel
{
    public int TransactionId { get; set; }
    public string? TransactionReference { get; set; }
    public decimal PaidAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string MatchHint { get; set; } = string.Empty;
}
