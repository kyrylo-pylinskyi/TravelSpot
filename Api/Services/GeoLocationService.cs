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

                    address.Add("PlaceId", result.ContainsKey("place_id") ? (string)result["place_id"] : string.Empty);
                    address.Add("OsmId", result.ContainsKey("osm_id") ? (string)result["osm_id"] : string.Empty);

                    JToken addressToken;
                    string country = string.Empty;
                    string city = string.Empty;
                    string state = string.Empty;
                    string street = string.Empty;

                    if (result.TryGetValue("address", out addressToken) && addressToken is JObject addressObject)
                    {
                        country = addressObject.ContainsKey("country") ? (string)addressObject["country"] : string.Empty;
                        city = addressObject.ContainsKey("city") ? (string)addressObject["city"] : string.Empty;
                        state = addressObject.ContainsKey("state") ? (string)addressObject["state"] : string.Empty;
                        street = addressObject.ContainsKey("road") ? (string)addressObject["road"] : string.Empty;
                    }

                    address.Add("Country", country);
                    address.Add("City", city);
                    address.Add("State", state);
                    address.Add("Street", street);
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
