using System.Net;
using System.Net.Mail;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
                SendEmail();

                _logger.LogInformation("Email sent at: {time}", DateTimeOffset.Now);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        private void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sujithsubramanyan25@gmail.com");
                mail.To.Add("sujithsubramanyan25@gmail.com");
                mail.Subject = "Hourly Email";
                mail.Body = "This email is sent every hour.";

               // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
               // smtp.Credentials = new NetworkCredential("your@email.com", "password");
               // to do get correct verification
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("your@email.com", "password");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Something Went Wrong: {exception}", ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}