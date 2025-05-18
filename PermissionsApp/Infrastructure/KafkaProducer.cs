using Confluent.Kafka;
using PermissionsApp.Infrastructure;
using System.Text.Json;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendMessageAsync(string topic, object value)
    {
        var messageJson = JsonSerializer.Serialize(value);

        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = messageJson
        };

        await _producer.ProduceAsync(topic, message);
    }

}

