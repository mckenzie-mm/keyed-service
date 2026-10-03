namespace payment;

public class StripePaymentService : IPaymentService
{
    public string ProcessPayment(decimal amount)
    {
        return $"Processing ${amount} via Stripe.";
    }
}