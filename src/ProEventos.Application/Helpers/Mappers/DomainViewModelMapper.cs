using AutoMapper;

using ProEventos.Application.ViewModels;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers.Mappers
{
    public class DomainViewModelMapper : Profile
    {
        public DomainViewModelMapper()
        {
            CreateMap<Event, EventViewModel>();
            CreateMap<Batch, BatchViewModel>();
            CreateMap<SocialNetwork, SocialNetworkViewModel>();
            CreateMap<Lecturer, LecturerViewModel>();
        }
    }
}