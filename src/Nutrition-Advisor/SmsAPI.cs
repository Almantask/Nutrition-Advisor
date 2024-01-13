namespace Nutrition_Advisor
{
    public class SmsAPI
    {
        public void SendSms(string body, string recipient)
        {
            ErrorSimulator.RunWithTransientError(() => Console.WriteLine($"Sending SMS to {recipient} with body:{Environment.NewLine}{body}"));
        }

        public Task SendSmsAsync(string body, string recipient)
        {
            SendSms(body, recipient);
            return Task.CompletedTask;
        }
    }
}