namespace payment;

public interface IPaymentService
{
    string ProcessPayment(decimal amount);
}
