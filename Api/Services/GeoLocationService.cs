using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Api.Services
{
    public static class GeoLocationService
    {
        public static async Task<Dictionary<string, string>> GetAddress(double latitude, double longitude)
        {
            Dictionary<string, string> address = new Dictionary<string, string>();
            // Construct the URL for the API request
            string apiUrl = $"https://nominatim.openstreetmap.org/reverse?lat={latitude}&lon={longitude}&format=json";

            // Create an HttpClient to send the request
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

                // Send the GET request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string content = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    // Assuming you have Newtonsoft.Json NuGet package installed
                    JObject result = JObject.Parse(content);

                    address.Add("PlaceId", (string)result["place_id"] );
                    address.Add("OsmId", (string)result["osm_id"]);
                    address.Add("Country", (string)result["address"]["country"]);
                    address.Add("City", (string)result["address"]["city"]);
                    address.Add("State", (string)result["address"]["state"]);
                    address.Add("Street", (string)result["address"]["road"]);
                }
                else
                {
                    throw new Exception($"Address not found : response {response.StatusCode}");
                }

                return address;
            }
        }
    }
}
