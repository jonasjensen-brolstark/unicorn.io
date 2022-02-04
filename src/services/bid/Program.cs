using Elastic.Apm.NetCoreAll;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "bid")
    .Enrich.WithElasticApmCorrelationInfo()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
        new Uri("http://elasticsearch:9200"))
    {
        CustomFormatter = new EcsTextFormatter()
    })
    );

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddDbContext<BidContext>(options => options.UseSqlServer(builder.Configuration["BID_CONNECTION_STRING"] ?? "DEFAULT_CONNECTION_STRING"));

var app = builder.Build();

UpdateDatabase();

// Configure the HTTP request pipeline.
app.UseAllElasticApm(app.Configuration);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void UpdateDatabase()
{
    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<BidContext>())
        {
            Console.WriteLine("migrating db");
            context?.Database.Migrate();
        }
    }
}