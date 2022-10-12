namespace ArchiAPI.Models
{
    public sealed class Config
    {
        public string NetString { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string LogPath { get; set; }
        public System.TimeSpan JwtTimeout { get; set; }
    }
}
