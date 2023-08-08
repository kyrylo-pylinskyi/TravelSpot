using Api.Models.Entities.Application;
using System.Linq;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotRateResponse
    {
        public static double CreateResponse(List<SpotRate> spotRates)
        {
            if (spotRates.Count() > 0)
                return spotRates.Select(r => (double)r.Rate).Average();
            else return 0;
        }
    }
}
