using Contracts.Events;
using ServiceB.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSMessageBus(builder =>
{
    builder.AddSQSPoller("https://sqs.us-east-1.amazonaws.com/821175633958/OrderQueue");
    builder.AddMessageHandler<OrderCreatedEventHandler, OrderCreatedEvent>(nameof(OrderCreatedEvent));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();