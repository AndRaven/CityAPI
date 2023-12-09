

using Microsoft.AspNetCore.Authorization.Infrastructure;

public class CitiesDataStore 
{
    public List<CityDto> Cities {get; set;}

    //public static CitiesDataStore Current {get;} = new CitiesDataStore(); 

    public CitiesDataStore ()
    {
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1, Name = "Melbourne", Description = "Windy city",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1, Name = "Spinning Wheel", Description = "A spinning wheel"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2, Name = "Yarra Valley", Description = "Wineries"
                    }
                }

            },

            new CityDto()
            {
                Id = 2, Name = "Adelaide", Description = "City of Hills",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1, Name = "Adelaide Hills", Description = "Some hills"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2, Name = "Barossa Valley", Description = "Wineries"
                    }
                }
            }
        };
    }


    
}