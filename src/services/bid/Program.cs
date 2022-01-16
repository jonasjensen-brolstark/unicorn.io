using Elastic.Apm.NetCoreAll;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "image")
    .Enrich.WithElasticApmCorrelationInfo()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
        new Uri("http://elasticsearch:9200"))
    {
        CustomFormatter = new EcsTextFormatter()
    })
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BidContext>(options =>   
  options.UseSqlServer(
    builder.Configuration.GetConnectionString("BID_CONNECTION_STRING")));

var app = builder.Build();

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


UpdateDatabase(app);

void UpdateDatabase(WebApplication app)
{
    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<BidContext>())
        {
            context?.Database.Migrate();
        }
    }
}