namespace Nutrition_Advisor
{
    public class SmsAPIAdapter : ISmsAdapter
    {
        private readonly SmsAPI smsApi;

        public SmsAPIAdapter()
        {
            smsApi = new SmsAPI();
        }

        public Task SendSmsNotificationAsync(string body, string recipient, CancellationToken ct)
        {
            return smsApi.SendSmsAsync(body, recipient);
        }
    }

}
