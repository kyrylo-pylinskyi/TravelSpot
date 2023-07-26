using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;

namespace Api.Models.DTO.Response.ProfileResponse
{
    public class UserInfoResponse
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserBio { get; set; }
        public byte[]? UserPhoto { get; set; }
        public List<Spot>? UserSpots { get; set; }
    }
}
