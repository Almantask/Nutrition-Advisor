namespace Nutrition_Advisor
{
    public interface IEmailAdapter
    {
        void SendEmailNotification(string body, string recipient);
    }

}
