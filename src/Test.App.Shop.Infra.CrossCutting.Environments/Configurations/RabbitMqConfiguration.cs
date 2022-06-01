namespace Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

public class RabbitMqConfiguration
{
    public string? Host { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? SubmittedOrderQueueName { get; set; }
}
