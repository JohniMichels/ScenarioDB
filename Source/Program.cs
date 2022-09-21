
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Serilog;
using Serilog.Events;
using ScenarioDB;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(settings => 
    {
        settings.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "ScenarioDB");
        opt.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
