namespace Starling.Domain.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}