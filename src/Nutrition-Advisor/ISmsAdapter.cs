namespace Nutrition_Advisor
{
    public interface ISmsAdapter
    {
        void SendSmsNotification(string body, string recipient);
    }

}
