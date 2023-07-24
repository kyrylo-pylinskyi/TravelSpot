namespace Api.Models.DTO.Response.Profile
{
    public class UserPhotoResponse
    {
        public string UserId { get; set; }
        public bool IsMainPhoto { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public byte[] Photo { get; set; }
    }
}
