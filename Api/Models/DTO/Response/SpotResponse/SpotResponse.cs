using Api.Models.Entities;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using System.Linq;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotResponse
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorPhoto { get; set; }
        public string SpotName { get; set; }
        public string SpotDescription { get; set; }
        public double SpotAvgRate { get; set; }
        public string SpotCategoryName { get; set; }
        public string SpotCategoryDescription { get; set; }
        public List<SpotTagResponse> SpotTags { get; set; }
        public List<SpotAddressResponse> SpotAddresses { get; set; }
        public List<SpotPhotoResponse> SpotPhotos { get; set; }

        public SpotResponse(Spot spot)
        {
            Id = spot.Id;
            AuthorId = spot.AuthorId;
            AuthorName = spot.Author?.UserName;
            AuthorPhoto = spot.Author?.Photos?.OrderByDescending(p => p.LastUpdateTime).FirstOrDefault()?.Photo;
            SpotName = spot.Name;
            SpotDescription = spot.Description;
            SpotCategoryName = spot.Category?.Name;
            SpotCategoryDescription = spot.Category?.Description;
            SpotAvgRate = SpotRateResponse.CreateResponse(spot.Rates);
            SpotTags = SpotTagResponse.CreateResponse(spot.Tags).ToList();
            SpotAddresses = SpotAddressResponse.CreateResponse(spot.Addresses).ToList();
            SpotPhotos = SpotPhotoResponse.CreateResponse(spot.Photos).ToList();
        }

        public static IEnumerable<SpotResponse> CreateResponse(List<Spot> spots)
        {
            foreach (var spot in spots)
            {
                yield return new SpotResponse(spot);
            }
        }

    }
}
