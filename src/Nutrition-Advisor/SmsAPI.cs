namespace Nutrition_Advisor
{
    public class SmsAPI
    {
        public void SendSms(string body, string recipient)
        {
            Console.WriteLine($"Sending SMS to {recipient} with body: {body}");
        }
    }
}