using Microsoft.AspNetCore.Mvc;
using Footlink.Domain.Entities;
using Footlink.Domain.Interfaces;

namespace Footlink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            var companies = await _companyRepository.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Company company)
        {
            await _companyRepository.AddAsync(company);
            await _companyRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }
    }
}
