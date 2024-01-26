
using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[Route("api/cities/{cityId}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
   private readonly ILogger<PointsOfInterestController> _logger;
    private readonly ICityInfoRepository _cityInfoRepository;

    private readonly IMapper _mapper;

    //constructor injectors
    public PointsOfInterestController( ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository, IMapper mapper)
   {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
    {
        if (! await _cityInfoRepository.CityExistsAsync(cityId))
        {
          _logger.LogInformation("City with {id} was not found.", cityId);

          return NotFound();
        }

         var pointOfInterestsFound = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
         
         return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestsFound));     
    }


   [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
       if (! await _cityInfoRepository.CityExistsAsync(cityId))
        {
          _logger.LogInformation("City with {id} was not found.", cityId);

          return NotFound();
        }

       var pointFound = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

       if (pointFound == null)
       {
         return NotFound();
       }

       return Ok(_mapper.Map<PointOfInterestDto>(pointFound));
    }

   [HttpPost]
    public async Task<ActionResult> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest)
    {
      //content assumed to come [FromBody]

      //check the city exists
      var city = await _cityInfoRepository.GetCityAsync(cityId);

      if (city == null)
      {
         return NotFound();
      }

      var pointOfInterestToAdd = _mapper.Map<PointOfInterest>(pointOfInterest);

     await _cityInfoRepository.AddPointOfInterestToCityAsync(cityId, pointOfInterestToAdd);
     await _cityInfoRepository.SaveChangesAsync();

     //map the entity from the database to the DTO class
     var pointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(pointOfInterestToAdd);

      return CreatedAtRoute("GetPointOfInterest", 
        new 
        {
           cityId = cityId,
           pointOfInterestId = pointOfInterestToReturn.Id
        },
        pointOfInterestToReturn);

    }

   // [HttpPut("{pointofinterestid}")]
   //  public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdateDto)
   //  {
   //    //check the city exists
   //    var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

   //    if (city == null)
   //    {
   //       return NotFound();
   //    }

   //    //find point of interest
   //    var pointOfInterestFound = city.PointsOfInterest.FirstOrDefault(pInterest => pInterest.Id == pointOfInterestId);

   //    if (pointOfInterestFound == null)
   //    {
   //       return NotFound();
   //    }

   //    //set all fields on the point of interest found
   //    pointOfInterestFound.Name = pointOfInterestUpdateDto.Name;
   //    pointOfInterestFound.Description = pointOfInterestUpdateDto.Description;

   //    return NoContent();

   //  }

   //  [HttpPatch("{pointofinterestid}")]
   //  public ActionResult  PartiallyUpdatePointOfInterest(int cityId, int pOfInterestId,
   //  [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDocument )
   //  {
   //     //check the city exists
   //    var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

   //    if (city == null)
   //    {
   //       return NotFound();
   //    }

   //    //find point of interest
   //    var pointOfInterestFound = city.PointsOfInterest.FirstOrDefault(pInterest => pInterest.Id == pOfInterestId);

   //    if (pointOfInterestFound == null)
   //    {
   //       return NotFound();
   //    }

   //    var pointOfInterestToPatch = new PointOfInterestUpdateDto()
   //    {
   //       Name = pointOfInterestFound.Name,
   //       Description = pointOfInterestFound.Description
   //    };

   //    patchDocument.ApplyTo<PointOfInterestUpdateDto>(pointOfInterestToPatch, ModelState);

   //    //check that the incoming patchDocumnet request is valid
   //    if (!ModelState.IsValid)
   //    {
   //       return BadRequest(ModelState);
   //    }

   //    //check that the updated pointOfInterestToPatch is valid
   //    if (!TryValidateModel(pointOfInterestToPatch))
   //    {
   //       return BadRequest(ModelState);
   //    }

   //   pointOfInterestFound.Name = pointOfInterestToPatch.Name;
   //   pointOfInterestFound.Description = pointOfInterestToPatch.Description;


   //    return NoContent();
   //  }
}