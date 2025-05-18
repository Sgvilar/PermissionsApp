using System.Threading.Tasks;

public interface IKafkaProducer
{
    Task SendMessageAsync(string topic, object value);
}