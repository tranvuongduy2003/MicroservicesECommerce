using Contracts.Configurations;

namespace Infrastructure.Configurations
{
    public class SmtpEmailSettings : IEmailSMTPSettings
    {
        public string DisplayName { get; set; }
        public string EnableVerification { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public bool UseSsl { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
