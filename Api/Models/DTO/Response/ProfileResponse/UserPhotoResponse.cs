namespace Api.Models.DTO.Response.ProfileResponse
{
    public class UserPhotoResponse
    {
        public string UserId { get; set; }
        public int PhotoId { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public byte[] Photo { get; set; }
    }
}
