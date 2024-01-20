using AutoMapper;

public class CityProfile : Profile
{

    public CityProfile()
    {
        CreateMap<City, CityWithoutPointsOfInterestDto>();
        CreateMap<City, CityDto>();
    }
}