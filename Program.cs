
using payment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register implementations with unique string keys
builder.Services.AddKeyedTransient<IPaymentService, PaypalPaymentService>("paypal");
builder.Services.AddKeyedTransient<IPaymentService, StripePaymentService>("stripe");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
