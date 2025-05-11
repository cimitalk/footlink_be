using Footlink.Domain.Entities;
using Footlink.Domain.Interfaces;
using Footlink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footlink.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly FootlinkDbContext _context;

        public CompanyRepository(FootlinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.Include(c => c.Users).ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
