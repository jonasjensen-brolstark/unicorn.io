namespace EventService.Nats;

using System;
using System.Text;
using NATS.Client;
using NATS.Client.JetStream;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class NatsEventService : IEventService, IDisposable
{
    private readonly ILogger<NatsEventService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private readonly string _streamName;

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private IConnection _natsConnection;

    public NatsEventService(ILogger<NatsEventService> logger, IServiceScopeFactory scopeFactory, IEventBusSubscriptionsManager subsManager, string streamName = "unicorn-io-stream")
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _streamName = streamName;
        _subsManager = subsManager;
        Options connectionOptions = ConnectionFactory.GetDefaultOptions();
        connectionOptions.Servers = new string[] { "nats://nats:4222" };
        _natsConnection = new ConnectionFactory().CreateConnection(connectionOptions);

        var jetStreamManagement = _natsConnection.CreateJetStreamManagementContext();


        var streamInfo = GetStreamInfoOrNullWhenNotExist(jetStreamManagement, _streamName);

        if (streamInfo == null)
        {
            StreamConfiguration streamConfig = StreamConfiguration.Builder()
                           .WithName(_streamName)
                           .WithSubjects("*")
                           .WithStorageType(StorageType.Memory)
                           .Build();

            jetStreamManagement.AddStream(streamConfig);
        }

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    public void Dispose()
    {
        _natsConnection.Dispose();

        _subsManager.Clear();
    }

    public void Publish(Event @event)
    {
        var subject = @event.GetType().Name;

        try
        {
            // create a JetStream context
            IJetStream jetStream = _natsConnection.CreateJetStreamContext();

            var data = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), _jsonSerializerOptions);
            Msg msg = new Msg(subject, null, null, data);
            PublishAck pa = jetStream.Publish(msg);
            _logger.LogInformation("Published message '{0}' on subject '{1}', stream '{2}', seqno '{3}'.",
                Encoding.UTF8.GetString(data), subject, pa.Stream, pa.Seq);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish event: {@event}", @event);
        }
    }

    public void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        var subject = _subsManager.GetEventKey<T>();

        var hasSubscription = _subsManager.HasSubscriptionsForEvent<T>();
        if (!hasSubscription)
        {
            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", subject, typeof(TH).GetGenericTypeName());

            var jetStream = _natsConnection.CreateJetStreamContext();
            var consumerConfig = ConsumerConfiguration.Builder()
                            .WithAckPolicy(AckPolicy.All) // don't want to worry about acking messages.
                            .Build();

            var pushSubOptions = PushSubscribeOptions.Builder()
                .WithConfiguration(consumerConfig)
                .WithStream(_streamName)
                .Build();

            jetStream.PushSubscribeAsync(subject, Guid.NewGuid().ToString(), async (_, args) => await HandleMessage<T, TH>(args), false, pushSubOptions);

        }
    }

    public void Unsubscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _subsManager.RemoveSubscription<T, TH>();
    }

    private async Task HandleMessage<T, TH>(MsgHandlerEventArgs args)
        where T : Event
        where TH : IEventHandler<T>
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var handler = scope.ServiceProvider.GetService<TH>();
            var @event = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(args.Message.Data), _jsonSerializerOptions);

            await handler?.Handle(@event);

            args.Message.Ack();
        }
    }

    private StreamInfo? GetStreamInfoOrNullWhenNotExist(IJetStreamManagement jsm, string streamName)
    {
        try
        {
            return jsm.GetStreamInfo(streamName);
        }
        catch (NATSJetStreamException e)
        {
            if (e.ErrorCode == 404)
            {
                return null;
            }
            throw;
        }
    }
}