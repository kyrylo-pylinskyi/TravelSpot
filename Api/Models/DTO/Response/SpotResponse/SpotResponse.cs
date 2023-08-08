using Api.Models.Entities;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotResponse
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public UserPhoto AuthorPhoto { get; set; }
        public string SpotName { get; set; }
        public string SpotDescription { get; set; }
        public double AvgRate { get { return Rates.Select(r => r.Rating).Average(); } }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<SpotTagResponse> Tags { get; set; }
        public List<SpotAddressResponse> Addresses { get; set; }
        public List<SpotRatingResponse> Rates { get; set; }
        public List<SpotPhotoResponse> Photos { get; set; }

        public SpotResponse(Spot spot)
        {
            Id = spot.Id;
            AuthorId = spot.AuthorId;
            AuthorName = spot.Author.UserName;
            AuthorPhoto = spot.Author.Photos.OrderByDescending(p => p.LastUpdateTime).FirstOrDefault();
        }
    }
}
