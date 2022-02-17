using AutoMapper;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

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
            CreateMap<UserViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
        }
    }
}