

using Microsoft.AspNetCore.Authorization.Infrastructure;

public class CitiesDataStore 
{
    public List<CityDto> Cities {get; set;}

    public static CitiesDataStore Current {get;} = new CitiesDataStore(); 

    public CitiesDataStore ()
    {
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1, Name = "Melbourne", Description = "Windy city"

            },

            new CityDto()
            {
                Id = 2, Name = "Adelaide", Description = "City of Hills"
            }
        };
    }


    
}