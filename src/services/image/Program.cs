using System;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace image
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .UseSerilog((context, services, configuration) => configuration
                //         .ReadFrom.Configuration(context.Configuration)
                //         .ReadFrom.Services(services)
                //         .Enrich.FromLogContext()
                //         .Enrich.WithProperty("Application", "image")
                //         .Enrich.WithElasticApmCorrelationInfo()
                //         .WriteTo.Console()
                //         .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
                //         {
                //             CustomFormatter = new EcsTextFormatter()
                //         }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
