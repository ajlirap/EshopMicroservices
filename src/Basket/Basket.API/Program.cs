var builder = WebApplication.CreateBuilder(args);

//1 . Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);

    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
}); 


var app = builder.Build();

//2. Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
