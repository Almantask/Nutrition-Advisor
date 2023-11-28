namespace Nutrition_Advisor
{
    public class EmailAPIAdapter : IEmailAdapter
    {
        private readonly EmailAPI emailApi;

        public EmailAPIAdapter()
        {
            emailApi = new EmailAPI();
        }

        public void SendEmailNotification(string body, string recipient)
        {
            emailApi.SendEmail(body, recipient);
        }
    }

}
