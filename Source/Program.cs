
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Serilog;
using Serilog.Events;
using ScenarioDB;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using ScenarioDB.Utils;
using ScenarioDB.Repositories;

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
        // settings.SerializerSettings.Converters.Add(new FlagEnumStringArrayConverter<JSchemaType>());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSingleton<ISchemaPathRepository, MockRepository>()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .AddSwaggerGen(opt =>
    {
        // opt.SwaggerDoc("v1", new() { Title = "ScenarioDB", Version = "v1" });
        opt.MapType<JSchemaType>(() =>
        {

            var singleItemSchema = new OpenApiSchema
            {
                Type = "string",
                Enum = Enum.GetNames(typeof(JSchemaType)).Select(x => new OpenApiString(x.ToLower())).ToArray()
            };
            var multipleItemSchema = new OpenApiSchema
            {
                Type = "array",
                Items = singleItemSchema
            };
            return new OpenApiSchema
            {
                OneOf = new List<OpenApiSchema>
                {
                    singleItemSchema,
                    multipleItemSchema
                }
            };
        });
    })
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
