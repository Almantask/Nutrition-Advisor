namespace Nutrition_Advisor
{
    public interface INotificationsFacade
    {
        ValueTask SendEmailNotificationAsync(string body, string recipient);
        ValueTask SendSmsNotificationAsync(string body, string recipient);
    }
}