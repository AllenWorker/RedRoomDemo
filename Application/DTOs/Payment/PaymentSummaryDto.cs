namespace RedRoomDemo.Application.DTOs.Payment;

public class PaymentSummaryDto
{
    public int TransactionId { get; set; }
    public string? TransactionReference { get; set; }
    public decimal PaidAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string WarningMessage { get; set; } = string.Empty;
}
