using Contracts.Configurations;

namespace Infrastructure.Configurations
{
    public class SmtpEmailSettings : IEmailSMTPSettings
    {
        public string DisplayName { get; set; }
        public string EnableVerification { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public string UseSsl { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
