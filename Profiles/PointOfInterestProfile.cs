using AutoMapper;

public class PointOfInterestProfile : Profile
{
   public PointOfInterestProfile()
   {
      //create a mapping configuration between PointOfInterest and PointOfInterestDto from source on the left to destination on the right
      CreateMap<PointOfInterest, PointOfInterestDto>();
      CreateMap<PointOfInterestCreationDto, PointOfInterest>();
      CreateMap<PointOfInterestUpdateDto, PointOfInterest>();
      CreateMap<PointOfInterest, PointOfInterestUpdateDto>();
   }
   
}