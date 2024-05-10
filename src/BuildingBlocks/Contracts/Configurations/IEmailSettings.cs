﻿namespace Contracts.Configurations
{
    public interface IEmailSMTPSettings
    {
        string DisplayName { get; set; }
        string EnableVerification { get; set; }
        string From { get; set; }
        string SmtpServer { get; set; }
        bool UseSsl { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
