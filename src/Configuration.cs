namespace FluxusApi;

public static class Configuration
{
    public static string JwtKey { get; set; }
    public static SmtpConfiguration Smtp = new();
    
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TrelloEmail { get; set; }
    }
    
}