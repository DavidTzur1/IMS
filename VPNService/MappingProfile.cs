using AutoMapper;
using VPNService.Entities.DTO;
using VPNService.Entities.Models;

namespace VPNService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Screening, ScreeningReadingDto>();
        }
    }
}
