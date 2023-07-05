namespace Api.Services.Smtp.Request
{
    public class VerificationRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
    }
}
