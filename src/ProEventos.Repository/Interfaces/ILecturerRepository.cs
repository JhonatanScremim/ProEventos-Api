using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Repository.Interfaces
{
    public interface ILecturerRepository
    {
         Task<IEnumerable<Lecturer>> GetAllLecturersAsync(bool includeEvents = false);
         Task<IEnumerable<Lecturer>> GetAllLecturersByNameAsync(string name, bool includeEvents = false);
         Task<Lecturer> GetLecturerByIdAsync(int id, bool includeEvents = false);
    }
}