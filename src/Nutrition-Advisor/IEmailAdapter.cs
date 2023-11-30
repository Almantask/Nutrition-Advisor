namespace Nutrition_Advisor
{
    public interface IEmailAdapter
    {
        Task SendEmailNotificationAsync(string body, string recipient, CancellationToken ct);
    }

}
