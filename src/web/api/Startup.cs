using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Elastic.Apm.NetCoreAll;
using EventService;
using EventService.Nats;

namespace api
{
    public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton<IEventService, NatsEventService>(sp =>
      {
          var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
          var logger = sp.GetRequiredService<ILogger<NatsEventService>>();
          var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

          return new NatsEventService(logger, serviceScopeFactory, eventBusSubcriptionsManager);
      });

      services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

      services.AddCors(o => o.AddPolicy("TotalPublic", builder =>
      {
        builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
      }));

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseAllElasticApm(Configuration);
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors("TotalPublic");
      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
