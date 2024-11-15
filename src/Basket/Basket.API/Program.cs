var builder = WebApplication.CreateBuilder(args);

//1 . Add services to the container.

var app = builder.Build();

//2. Configure the HTTP request pipeline.

app.Run();
