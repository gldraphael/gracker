namespace Gracker.ServiceShell;

internal class MessagingConfig
{
    public string Host { get; set; } = "--localhost--";
    public ushort Port { get; set; } = 5672;
    public string VirtualHost { get; set; } = "/";
    public string Username { get; set; } = "--guest--";
    public string Password { get; set; } = "--password--";

    public bool IsValid => !(
           Host.StartsWith("--", StringComparison.InvariantCulture) 
        || Username.StartsWith("--", StringComparison.InvariantCulture) 
        || Password.StartsWith("--", StringComparison.InvariantCulture)
    );
}
