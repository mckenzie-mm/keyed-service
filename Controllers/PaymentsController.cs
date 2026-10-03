using Microsoft.AspNetCore.Mvc;

using payment;

namespace keyed_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private IServiceProvider _serviceProvider;

    public PaymentsController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet("process")]
    public string GetPayment(string method, int amount)
    {
        // Dynamically fetch the matching service based on a runtime string variable
       IPaymentService paymentService = _serviceProvider.
        GetRequiredKeyedService<IPaymentService>(method.ToLower());

        if (method != "stripe" && method != "paypal")
        {
            return "unrecognized payment method";
        }

        string response = paymentService.ProcessPayment(amount);
        return response;
    }
}
