# Keyed Services

Keyed services (introduced in .NET 8) allow you to register multiple implementations of the same interface and retrieve a specific one using a unique identifier or "key" (such as a string, enum, or object). 

Here is a complete, real-world example of how to implement, register, and resolve keyed services in a C# application using an order processing payment scenario. 

### 1. The Interface and Implementations

First, define a common interface and create multiple concrete implementations for it. 

```cs
// The common interface
public interface IPaymentService
{
    void ProcessPayment(decimal amount);
}

// Implementation #1
public class PaypalPaymentService : IPaymentService
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via PayPal.");
    }
}

// Implementation #2
public class StripePaymentService : IPaymentService
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via Stripe.");
    }
}
```
### 2. Service Registration (Program.cs)
Register the implementations into your DI container using AddKeyedSingleton, AddKeyedScoped, or AddKeyedTransient. 

Assign a unique key to each implementation. 

```cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Register implementations with unique string keys
builder.Services.AddKeyedTransient<IPaymentService, PaypalPaymentService>("paypal");
builder.Services.AddKeyedTransient<IPaymentService, StripePaymentService>("stripe");


var host = builder.Build();
```
### 3. Resolving the Keyed Services
There are two primary ways to consume keyed services: via Constructor Injection or via Runtime/Service Provider Resolution. 

Option A: Constructor Injection (Static Assignment)

If a controller needs a specific implementation, use the [FromKeyedServices] attribute in the constructor: 
```cs
...., [FromKeyedServices("stripe")] IPaymentService paymentService, ...
```

Option B: Dynamic Runtime Resolution (Service Provider)

If you do not know which service you need until runtime (e.g., based on a user selection or configuration database), resolve it using the GetKeyedService or GetRequiredKeyedService extensions. 

 ```cs
    // Dynamically fetch the matching service based on a runtime string variable
    var paymentService = serviceProvider.GetRequiredKeyedService<IPaymentService>(paymentMethod.ToLower());
    
    paymentService.ProcessPayment(total);
 
```
Best Practice Tip: Use Enums instead of Strings

To avoid typos and hardcoded string values across your codebase, you can pass enums as keys instead of strings: 


 ```cs
csharp
public enum PaymentType { Paypal, Stripe }

// Registration
builder.Services.AddKeyedTransient<IPaymentService, PaypalPaymentService>(PaymentType.Paypal);

// Constructor Injection
public class OrderProcessor([FromKeyedServices(PaymentType.Paypal)] IPaymentService paymentService) { ... }
```






