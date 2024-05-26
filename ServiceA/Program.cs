using AWS.Messaging.Publishers.SQS;
using Contracts.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSMessageBus(builder =>
{
    builder.AddSQSPublisher<OrderCreatedEvent>("https://sqs.us-east-1.amazonaws.com/821175633958/OrderQueue", nameof(OrderCreatedEvent));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/order", async (ISQSPublisher publisher) =>
{
    // call order creation service
    var orderId = Guid.NewGuid();
    var customerId = Guid.NewGuid();

    // create order created event
    var orderCreatedEvent = new OrderCreatedEvent(orderId, customerId);

    // publish the message
    await publisher.SendAsync(orderCreatedEvent);

    return Results.Created();
});

app.UseHttpsRedirection();

app.Run();