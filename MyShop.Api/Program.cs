using MyShop.Api.Extensions;
using MyShop.IoC;
using MyShop.Shared.Enums;
using MyShop.Shared.Extensions;
using MyShop.Shared.SysConfigs;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((ctx, services, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration)
       .ReadFrom.Services(services)
       .Enrich.FromLogContext();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


ConfigurationManager configuration = builder.Configuration;
ConfigManager.Initial(configuration);

builder.Services.RegisterService(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // => /openapi/v1.json
    app.MapScalarApiReference("/docs", options =>
    {
        options.Title = "MyShop API Docs";
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseApiLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
