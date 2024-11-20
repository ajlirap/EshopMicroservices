using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//------------------------------
//Infrastructure -EF Core
//Application - MediatR
//API - Carter, HealthChecks, ....

builder.Services
       .AddApplicationServices()
       .AddInfrastructureServices(builder.Configuration)
       .AddApiServices();
//------------------------------

//1 . Add services to the container.
var app = builder.Build();

//2. Configure the HTTP request pipeline.
app.Run();