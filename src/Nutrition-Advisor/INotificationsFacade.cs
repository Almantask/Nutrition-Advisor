namespace Nutrition_Advisor
{
    public interface INotificationsFacade
    {
        void SendEmailNotification(string body, string recipient);
        void SendSmsNotification(string body, string recipient);
    }
}