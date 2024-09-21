using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PokemonAPI.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _pokeApiBaseUrl;

        public PokemonController(HttpClient httpClient, IOptions<PokeApiSettings> pokeApiSettings)
        {
            _httpClient = httpClient;
            _pokeApiBaseUrl = pokeApiSettings.Value.BaseUrl;
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemons([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_pokeApiBaseUrl}pokemon?limit={limit}&offset={offset}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from PokeAPI.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log exception (optional)
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemonByName(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_pokeApiBaseUrl}pokemon/{name}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from PokeAPI.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log exception (optional)
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
    
