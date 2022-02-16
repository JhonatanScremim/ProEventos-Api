using System.Threading.Tasks;
using ProEventos.Application.ViewModels;

namespace ProEventos.Application.Interfaces
{
    public interface ITokenService
    {
         Task<string> CreateToken(UserUpdateViewModel userViewModel);
    }
}