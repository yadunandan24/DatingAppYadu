using API.DTO_s;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl,  //for a particular member to match to a diff prop,
                 options => options.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age,
                 options => options.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
        }
    }
}
