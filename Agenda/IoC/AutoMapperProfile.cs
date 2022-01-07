using AutoMapper;

namespace App.RestfulAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DTO.Status, Models.Status>().ReverseMap();
            CreateMap<DTO.Schedule.Activity, Models.Schedule.Activity>().ReverseMap();
            CreateMap<DTO.Schedule.ActivityDetail, Models.Schedule.Activity>().ReverseMap();
            CreateMap<DTO.Schedule.Property, Models.Schedule.Property>().ReverseMap();
            CreateMap<DTO.Schedule.Survey, Models.Schedule.Survey>().ReverseMap();
        }
    }
}
