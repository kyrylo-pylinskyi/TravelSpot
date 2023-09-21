using Api.Models.DTO.Response.AtractionResponse;
using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Runtime.CompilerServices;

namespace Api.Services.Helpers
{
    public static class CityAtractionsService
    {
        private static string urlBase = "https://fajnepodroze.pl/";

        public static async Task<List<AtractionResponse>> GetCityAtractions(string city)
        {
            string url = urlBase + city;

            var response = new List<AtractionResponse>();
            string[] substringsToRemove = { "Zobacz", "Zwiedź" };

            using (WebClient client = new WebClient())
            {
                string html = client.DownloadString(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var titles = doc.DocumentNode.Descendants("h2");

                foreach (var title in titles)
                {
                    // var description = title.SelectSingleNode("following-sibling::p");
                    int index = title.InnerText.IndexOf('&');

                    string titleText = index == -1 ? title.InnerText.Substring(3).Trim() : title.InnerText.Substring(3, index - 3).Trim();

                    foreach(string  substring in substringsToRemove)
                    {
                        titleText = titleText.Replace(substring, "").Trim();
                    }

                    var placeId = await GeoLocationService.GetPlaceId(titleText.Replace(' ', '+'), city.Replace(' ', '+'));

                    var place = await GeoLocationService.GetPlace(placeId);

                    response.Add(new AtractionResponse()
                    {
                        Title = titleText.CapitalizeFirstLetter(),
                        PlaceId = place.PlaceId,
                        Latitude = place.Latitude, 
                        Longitude = place.Longitude,
                        City = place.City,
                        Street = place.Street,
                    });

                }

                return response.Where(r => r.PlaceId != null).ToList();
            }
        }

        static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

    }
}
