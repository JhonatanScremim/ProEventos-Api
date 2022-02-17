using AutoMapper;

using ProEventos.Application.ViewModels;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

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
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserUpdateViewModel>();
        }
    }
}