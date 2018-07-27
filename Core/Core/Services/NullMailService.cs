using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this.logger = logger;
        }
        public void SendMessage(string to,string subject,string body)
        {
            //Log the message
            this.logger.LogInformation($"To:{to} Subject:{subject} Body:{body}");
        }
    }
}
