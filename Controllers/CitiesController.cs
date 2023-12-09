using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityAPI.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    private readonly CitiesDataStore _citiesDataStore;

    public CitiesController(CitiesDataStore citiesDataStore)
    {
        _citiesDataStore = citiesDataStore;
    }
   [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return  Ok(_citiesDataStore.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityResult = _citiesDataStore.Cities.FirstOrDefault( city => city.Id == id);

        if (cityResult == null)
        {
            return NotFound();
        }

        //http helper method
        return  Ok(cityResult);
    }
}