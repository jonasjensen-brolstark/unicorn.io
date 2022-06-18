using Elastic.Apm.NetCoreAll;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using MediatR;
using EventService;
using EventService.Nats;
using bid.Events.Handlers;
using bid.Events;

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

builder.Services.AddDbContext<BidContext>(options => options.UseSqlServer(builder.Configuration["BID_CONNECTION_STRING"] ?? "DEFAULT_CONNECTION_STRING"));
RegistreEventService();

var app = builder.Build();
UpdateDatabase();

var eventService = app.Services.GetRequiredService<IEventService>();
eventService.Subscribe<BidPlacedEvent, BidPlacedEventHandler>();

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

void RegistreEventService()
{
    builder.Services.AddSingleton<IEventService, NatsEventService>(sp =>
    {
        var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        var logger = sp.GetRequiredService<ILogger<NatsEventService>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var eventService = new NatsEventService(logger, serviceScopeFactory, eventBusSubcriptionsManager);

        return eventService;
    });

    builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

    builder.Services.AddTransient<BidPlacedEventHandler>();
}