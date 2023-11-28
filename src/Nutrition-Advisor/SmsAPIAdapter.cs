namespace Nutrition_Advisor
{
    public class SmsAPIAdapter : ISmsAdapter
    {
        private readonly SmsAPI smsApi;

        public SmsAPIAdapter()
        {
            smsApi = new SmsAPI();
        }

        public void SendSmsNotification(string body, string recipient)
        {
            smsApi.SendSms(body, recipient);
        }
    }

}
