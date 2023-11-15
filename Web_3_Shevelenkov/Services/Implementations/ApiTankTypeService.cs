using System.Text.Json;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Services.Implementations
{
    public class ApiTankTypeService : ITankTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger _logger;

        public ApiTankTypeService(HttpClient httpClient,
                                IConfiguration configuration,
                                ILogger<ApiTankService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public Task<ResponseData<List<TankType>>> GetTankTypeListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
