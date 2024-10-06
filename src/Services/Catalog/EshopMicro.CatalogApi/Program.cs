
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. DI 

//builder.Services.AddCarter(null, config => config.WithModules()); // Add Carter to the DI container
builder.Services.AddCarter(); // Add Carter to the DI container

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter(); // Add Carter to the request pipeline

app.Run();
