using Scraper.api.Helper;
using Scraper.api.Models;
using Scraper.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// bind key section of appsettings to ApplicationOptions class, through option pattern
builder.Services
    .AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.Key))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IScraperService, ScraperService>();
builder.Services.AddScoped<IHtmlParserService, HtmlParserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// close and flush log object at the end of program
LogHelper.Instance.CloseAndFlush();