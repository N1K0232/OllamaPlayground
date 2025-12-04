using OllamaPlayground.ApiService.BusinessLayer.Settings;
using OllamaPlayground.ApiService.Swagger;
using TinyHelpers.AspNetCore.Extensions;
using TinyHelpers.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var settings = builder.Services.ConfigureAndGet<AppSettings>(builder.Configuration, nameof(AppSettings)) ?? new AppSettings();
var swagger = builder.Services.ConfigureAndGet<SwaggerSettings>(builder.Configuration, nameof(SwaggerSettings)) ?? new SwaggerSettings();

builder.Services.AddRequestLocalization(settings.SupportedCultures);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultExceptionHandler();
builder.Services.AddDefaultProblemDetails();

if (swagger.IsEnabled)
{
    builder.Services.AddOpenApi(options =>
    {
        options.RemoveServerList();
        options.AddAcceptLanguageHeader();
        options.AddDefaultProblemDetailsResponse();
    });
}

var app = builder.Build();
app.Environment.ApplicationName = settings.ApplicationName;

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (swagger.IsEnabled)
{
    app.UseMiddleware<SwaggerBasicAuthenticationMiddleware>();
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", settings.ApplicationName);
        options.InjectStylesheet("/css/swagger.css");
    });
}

app.Run();