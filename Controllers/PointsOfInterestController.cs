
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[Route("api/cities/{cityId}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
   private readonly ILogger<PointsOfInterestController> _logger;
    private readonly CitiesDataStore _citiesDataStore;

    //constructor injectors
    public PointsOfInterestController( ILogger<PointsOfInterestController> logger, CitiesDataStore citiesDataStore)
   {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    }

    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
      try{
          //test expection secnario
          //throw new Exception("Sample exception");

         var cityFound = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

         if (cityFound == null)
         {
            _logger.LogInformation($"City with given {cityId} id was not found whne requesting points of interest");
            return NotFound();
         }

         return Ok(cityFound.PointsOfInterest);
      }
      catch(Exception ex)
      {
         _logger.LogCritical("Exception occured while accessing points of interest", ex);

         return StatusCode(500, "Error happened while handling request");
      }
       
    }

     [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
       var cityFound = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);


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
      var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

      if (city == null)
      {
         return NotFound();
      }

      var maxPointOfInterest = _citiesDataStore.Cities.SelectMany(city => city.PointsOfInterest).Max(p => p.Id);

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
      var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

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

    [HttpPatch("{pointofinterestid}")]
    public ActionResult  PartiallyUpdatePointOfInterest(int cityId, int pOfInterestId,
    [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDocument )
    {
       //check the city exists
      var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

      if (city == null)
      {
         return NotFound();
      }

      //find point of interest
      var pointOfInterestFound = city.PointsOfInterest.FirstOrDefault(pInterest => pInterest.Id == pOfInterestId);

      if (pointOfInterestFound == null)
      {
         return NotFound();
      }

      var pointOfInterestToPatch = new PointOfInterestUpdateDto()
      {
         Name = pointOfInterestFound.Name,
         Description = pointOfInterestFound.Description
      };

      patchDocument.ApplyTo<PointOfInterestUpdateDto>(pointOfInterestToPatch, ModelState);

      //check that the incoming patchDocumnet request is valid
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      //check that the updated pointOfInterestToPatch is valid
      if (!TryValidateModel(pointOfInterestToPatch))
      {
         return BadRequest(ModelState);
      }

     pointOfInterestFound.Name = pointOfInterestToPatch.Name;
     pointOfInterestFound.Description = pointOfInterestToPatch.Description;


      return NoContent();
    }
}