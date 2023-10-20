
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

     [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
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

   [HttpPost]
    public ActionResult CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest)
    {
      //content assumed to come [FromBody]

      //check the city exists
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);

      if (city == null)
      {
         return NotFound();
      }

      var maxPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(city => city.PointsOfInterest).Max(p => p.Id);

      var addedPointOfInterest = new PointOfInterestDto()
      {
         Id = ++maxPointOfInterest,
         Name = pointOfInterest.Name,
         Description = pointOfInterest.Description
      };

      city.PointsOfInterest.Add(addedPointOfInterest);

      return CreatedAtRoute("GetPointOfInterest", 
        new 
        {
           cityId = cityId,
           pointOfInterestId = addedPointOfInterest.Id
        },
        addedPointOfInterest);

    }

   [HttpPut("{pointofinterestid}")]
    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdateDto)
    {
      //check the city exists
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);

      if (city == null)
      {
         return NotFound();
      }

      //find point of interest
      var pointOfInterestFound = city.PointsOfInterest.FirstOrDefault(pInterest => pInterest.Id == pointOfInterestId);

      if (pointOfInterestFound == null)
      {
         return NotFound();
      }

      //set all fields on the point of interest found
      pointOfInterestFound.Name = pointOfInterestUpdateDto.Name;
      pointOfInterestFound.Description = pointOfInterestUpdateDto.Description;

      return NoContent();

    }
}