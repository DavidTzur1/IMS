using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;
using VPN.Models;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IVPNRepository _reposity;
        private readonly ILogger<CompaniesController> logger;
        public CompaniesController(IVPNRepository reposity, ILogger<CompaniesController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _reposity.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _reposity.GetCompany(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

            [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyModel company)
        {
            try
            {
                await _reposity.CreateCompany(company);
                return Ok(200);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyModel company)
        {
            try
            {
                var dbCompany = await _reposity.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                await _reposity.UpdateCompany(id, company);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
