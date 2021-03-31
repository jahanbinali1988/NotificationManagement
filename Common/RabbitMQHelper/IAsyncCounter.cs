namespace RabbitMQHelper
{
    public interface IAsyncCounter<T> where T : CountAggregatorMessage
    {
        void Start();

        void Stop();
    }
}