namespace Nutrition_Advisor
{
    public class NotificationsFacade : INotificationsFacade
    {
        private readonly IEmailAdapter _emailAdapter;
        private readonly ISmsAdapter _smsAdapter;

        public NotificationsFacade(IEmailAdapter emailAdapter, ISmsAdapter smsAdapter)
        {
            _emailAdapter = emailAdapter;
            _smsAdapter = smsAdapter;
        }

        public void SendEmailNotification(string body, string recipient)
        {
            _emailAdapter.SendEmailNotification(body, recipient);
        }

        public void SendSmsNotification(string body, string recipient)
        {
            _smsAdapter.SendSmsNotification(body, recipient);
        }
    }

}
