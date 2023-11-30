namespace Nutrition_Advisor
{
    public class EmailAPI
    {
        public void SendEmail(string body, string recipient)
        {
            Console.WriteLine($"Sending email to {recipient} with body: {body}");
        }

        public Task SendEmailAsync(string body, string recipient)
        {
            SendEmail(body, recipient);
            return Task.CompletedTask;
        }
    }
}