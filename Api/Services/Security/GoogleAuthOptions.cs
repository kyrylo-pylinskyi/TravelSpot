namespace Api.Services.Security
{
    public class GoogleAuthOptions
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string AuthUri { get; set; }
        public string TokenUri { get; set; }
        public string AuthProvider_x509_cert_url { get; set; }
        public string ClientSecret { get; set; }
        public string[] RedirectUris { get; set; }
    }
}
