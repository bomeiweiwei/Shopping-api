using Microsoft.AspNetCore.Diagnostics;
using MyShop.Api.Extensions;
using MyShop.IoC;
using MyShop.Models.Exceptions;
using MyShop.Models.Resp.Error;
using MyShop.Shared.Enums;
using MyShop.Shared.Extensions;
using MyShop.Shared.SysConfigs;
using Newtonsoft.Json;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// my windows pc local test, Staging = Dev, when change than use
//if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
//{
//    builder.Configuration.AddUserSecrets<Program>(optional: true);
//}

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
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;

            var traceId = context.TraceIdentifier;

            CustomErrorResponse errorResponse;
            if (ex is HttpStatusException httpEx)
            {
                context.Response.StatusCode = httpEx.HttpStatusCode;
                errorResponse = new CustomErrorResponse(httpEx.Message, (int)httpEx.AppStatusCode)
                {
                    TraceId = traceId
                };

                logger.LogWarning(
                    ex,
                    "Handled business exception. TraceId={TraceId}, Path={Path}",
                    traceId,
                    context.Request.Path
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse = new CustomErrorResponse(
                    "An unexpected error occurred.",
                    (int)ReturnCode.ExceptionError
                )
                {
                    TraceId = traceId
                };

                logger.LogError(
                   ex,
                   "Unhandled exception. TraceId={TraceId}, Path={Path}",
                   traceId,
                   context.Request.Path
               );
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseApiLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
