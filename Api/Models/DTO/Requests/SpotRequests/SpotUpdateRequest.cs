namespace Api.Models.DTO.Requests.SpotRequests
{
    public class SpotUpdateRequest
    {
        public int SpotId { get; set; }
        public string SpotName { get; set; }
        public string SpotDescription { get; set; }
    }
}
