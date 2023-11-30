namespace Nutrition_Advisor
{
    public interface ISmsAdapter
    {
        Task SendSmsNotificationAsync(string body, string recipient, CancellationToken ct);
    }

}
