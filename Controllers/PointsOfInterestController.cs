
using Microsoft.AspNetCore.Mvc;

[Route("api/cities/{cityId}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
       var cityFound = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);

       if (cityFound == null)
       {
          return NotFound();
       }

       return Ok(cityFound.PointsOfInterest);
    }

     [HttpGet("{pointOfInterestId}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
       var cityFound = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);


       if (cityFound == null)
       {
          return NotFound();
       }

       var pointFound = cityFound.PointsOfInterest.FirstOrDefault(pInterest => pInterest.Id == pointOfInterestId);

       if (pointFound == null)
       {
         return NotFound();
       }

       return Ok(pointFound);
    }
}