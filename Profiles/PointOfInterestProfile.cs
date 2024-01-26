using AutoMapper;

public class PointOfInterestProfile : Profile
{
   public PointOfInterestProfile()
   {
      //create a mapping configuration between PointOfInterest and PointOfInterestDto
      CreateMap<PointOfInterest, PointOfInterestDto>();
      CreateMap<PointOfInterestCreationDto, PointOfInterest>();
      CreateMap<PointOfInterestUpdateDto, PointOfInterest>();
   }
   
}