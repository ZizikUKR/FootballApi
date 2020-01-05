using FootballParser.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballParser.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FootballController : ControllerBase
    {
        private readonly IFootballService _footballService;
        public FootballController(IFootballService footballService)
        {
            _footballService = footballService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _footballService.GetStatistic();
            return Ok();
        }
    }
}
