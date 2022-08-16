
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNService.Contracts;
using VPNService.Entities.DTO;

namespace VPNService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ScreeningController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScreenings()
        {
            try
            {
                var screenings = await _repository.Screening.GetAllScreeningsAsync();
                _logger.LogInfo($"Returned all screeninings from database.");
                var dto = _mapper.Map<IEnumerable<ScreeningReadingDto>>(screenings);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllScreenings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ScreeningById")]
        public async Task<IActionResult> GetScreeningById(int id)
        {
            try
            {
                var screening = await _repository.Screening.GetScreeningByIdAsync(id);
                if (screening == null)
                {
                    _logger.LogError($"Screening with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned screening with id: {id}");

                    var dto = _mapper.Map<ScreeningReadingDto>(screening);
                    return Ok(dto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ScreeningById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
