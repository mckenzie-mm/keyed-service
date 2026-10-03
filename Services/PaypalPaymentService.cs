namespace payment;

// Implementation #1
public class PaypalPaymentService : IPaymentService
{
    public string ProcessPayment(decimal amount)
    {
        return $"Processing ${amount} via PayPal.";
    }
}