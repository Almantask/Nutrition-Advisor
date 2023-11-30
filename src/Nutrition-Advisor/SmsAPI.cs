namespace Nutrition_Advisor
{
    public class SmsAPI
    {
        public void SendSms(string body, string recipient)
        {
            Console.WriteLine($"Sending SMS to {recipient} with body: {body}");
        }

        public Task SendSmsAsync(string body, string recipient)
        {
            SendSms(body, recipient);
            return Task.CompletedTask;
        }
    }
}