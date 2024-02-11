using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityAPI.Controllers;

[ApiController]
//[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cities")]
public class CitiesController : ControllerBase
{

    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    const int maxCitiesPageSize = 20;

    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper) 
    {
       _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
       _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery, 
    int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize > maxCitiesPageSize)
        {
            pageSize = maxCitiesPageSize;
        }

        var (entityCities, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        
        return  Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(entityCities));
    }

    /// <summary>
    /// Get a city by id.
    /// </summary>
    /// <param name="id"> id of the city </param>
    /// <param name="includePointsOfInterest"> whether or not to include points of interest</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Returns the specified city</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
    {
        var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

        if (city == null)
        {
            return NotFound();
        }
        
        if (includePointsOfInterest)
        {
          return  Ok( _mapper.Map<CityDto>(city));
        }
        
        return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
    }
}