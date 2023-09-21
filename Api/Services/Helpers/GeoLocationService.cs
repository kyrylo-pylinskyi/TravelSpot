using Api.Services.DTO;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Api.Services.Helpers
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

        public static async Task<string> GetPlaceId(string locationName, string cityName)
        {
            // Construct the URL for the API request
            string apiUrl = $"http://nominatim.openstreetmap.org/search?q={locationName},+{cityName}&format=json";

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
                    JArray resultArray = JArray.Parse(content);

                    JObject result = (JObject)resultArray.FirstOrDefault();
                    if (result != null)
                    {
                        return result.ContainsKey("place_id") ? (string)result["place_id"] : string.Empty;
                    }
                }
            }

            return null;
        }

        public static async Task<GeoLocationModel> GetPlace(string externalId)
        {
            var location = new GeoLocationModel();

            var apiUrl = $"https://nominatim.openstreetmap.org/details.php?place_id={externalId}&format=json";

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

                    if (result.TryGetValue("addresstags", out JToken addressToken) && addressToken is JObject addressObject)
                    {
                        location.City = addressObject.ContainsKey("city") ? (string)addressObject["city"] : string.Empty;
                        location.Street = addressObject.ContainsKey("street") ? (string)addressObject["street"] : string.Empty;
                    }

                    if (result.TryGetValue("centroid", out JToken centroidToken) && centroidToken is JObject centroidObject)
                    {
                        if (centroidObject.TryGetValue("coordinates", out JToken coordinatesToken) && coordinatesToken is JArray coordinatesArray)
                        {
                            if (coordinatesArray.Count >= 2 && coordinatesArray[0].Type == JTokenType.Float && coordinatesArray[1].Type == JTokenType.Float)
                            {
                                location.Longitude = (double)coordinatesArray[0];
                                location.Latitude = (double)coordinatesArray[1];
                            }
                        }
                    }
                }
            }

            return location;
        }
    }
}
