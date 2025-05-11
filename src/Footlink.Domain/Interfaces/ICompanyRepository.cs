using Footlink.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Footlink.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task SaveChangesAsync();
    }
}
