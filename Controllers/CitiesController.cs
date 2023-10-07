using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityAPI.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
   [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return  Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityResult = CitiesDataStore.Current.Cities.FirstOrDefault( city => city.Id == id);

        if (cityResult == null)
        {
            return NotFound();
        }

        //http helper method
        return  Ok(cityResult);
    }
}