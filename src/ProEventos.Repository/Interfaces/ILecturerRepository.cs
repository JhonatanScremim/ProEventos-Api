using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Repository.Interfaces
{
    public interface ILecturerRepository
    {
         Task<Lecturer[]> GetAllLecturersAsync(bool includeEvents = false);
         Task<Lecturer[]> GetAllLecturersByNameAsync(string name, bool includeEvents = false);
         Task<Lecturer> GetLecturerByIdAsync(int id, bool includeEvents = false);
    }
}