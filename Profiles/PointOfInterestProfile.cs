using AutoMapper;

public class PointOfInterestProfile : Profile
{
   public PointOfInterestProfile()
   {
      CreateMap<PointOfInterest, PointOfInterestDto>();
   }
   
}