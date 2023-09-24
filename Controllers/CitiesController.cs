using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
   [HttpGet]
    public JsonResult GetCities()
    {
        return  new JsonResult(
            new List<Object> {
                new {Id = 1, Name = "Melbourne"},
                new {Id = 2, Name = "Sydney"}
            }
        );
    }
}