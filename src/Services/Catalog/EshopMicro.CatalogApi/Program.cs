

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. DI 

//builder.Services.AddCarter(null, config => config.WithModules()); // Add Carter to the DI container
builder.Services.AddCarter(); // Add Carter to the DI container

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomerExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter(); // Add Carter to the request pipeline

app.UseExceptionHandler(options => { });

app.Run();
