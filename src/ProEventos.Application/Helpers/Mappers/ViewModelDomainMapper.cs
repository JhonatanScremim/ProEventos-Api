using AutoMapper;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers.Mappers
{
    public class ViewModelDomainMapper : Profile
    {
        public ViewModelDomainMapper()
        {
            CreateMap<EventViewModel, Event>();
            CreateMap<BatchViewModel, Batch>();
            CreateMap<SocialNetworkViewModel, SocialNetwork>();
            CreateMap<LecturerViewModel, Lecturer>();
        }
    }
}