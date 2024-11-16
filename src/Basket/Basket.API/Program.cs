using EshopMicro.Common.Handler;
using Marten;

var builder = WebApplication.CreateBuilder(args);

//1 . Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);

    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts => 
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomerExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(opts => { });

//2. Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
