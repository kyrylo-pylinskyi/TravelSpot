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
                // Send the GET request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string content = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    // Assuming you have Newtonsoft.Json NuGet package installed
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    address.Add("Country", result.address.country);
                    address.Add("City", result.address.city);
                    address.Add("Street", result.address.road);
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
