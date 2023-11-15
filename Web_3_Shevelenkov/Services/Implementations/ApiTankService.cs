using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Services.Implementations
{
    public class ApiTankService : ITankService
    {
        private readonly HttpClient _httpClient;
        private readonly int _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger _logger;

        public ApiTankService(HttpClient httpClient,
                                IConfiguration configuration,
                                ILogger<ApiTankService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetValue<int>("ItemsPerPage");
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<Tank>> CreateProductAsync(Tank tank, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "tanks");
            //var response = await _httpClient.PostAsJsonAsync(uri, (product, formFile), _serializerOptions);

            using (var content = new MultipartFormDataContent())
            {
                // Serialize the "tank" object as JSON and add it to the request
                var tankJson = JsonConvert.SerializeObject(tank); // Make sure to include Newtonsoft.Json namespace
                content.Add(new StringContent(tankJson), "tank");

                /*// Add the image file to the request
                var imageContent = new StreamContent(formFile.OpenReadStream());
                content.Add(imageContent, "image", formFile.FileName);*/

                if (formFile != null)
                {
                    await SaveImageAsync(tank.Id, formFile);
                }

                var response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response
                        .Content
                        .ReadFromJsonAsync<ResponseData<Tank>>
                        (_serializerOptions);
                    return data;
                }
                _logger.LogError($"-----> object not created. Error:{response.StatusCode.ToString()}");
                return new ResponseData<Tank>
                {
                    Success = false,
                    ErrorMessage = $"Объект не добавлен. Error:{response.StatusCode.ToString()}"
                };
            }

            
        }

        public Task DeleteTankAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Tank>> GetTankByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ProductListModel<Tank>>> GetTankListAsync(string? categoryNormalizedName = null, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}tanks/");

            urlString.Append($"page{pageNo}");

            urlString.Append($"pageSize{_pageSize.ToString()}?");

            if (categoryNormalizedName != null)
            {
                urlString.Append($"tankType={categoryNormalizedName}");
            };


            // добавить номер страницы в маршрут

            // отправить запрос к API
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                    .Content
                    .ReadFromJsonAsync<ResponseData<ProductListModel<Tank>>>
                    (_serializerOptions);
                }
                catch (System.Text.Json.JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<ProductListModel<Tank>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
            return new ResponseData<ProductListModel<Tank>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error:{response.StatusCode.ToString()}"
            };
        }

        public async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}Dishes/{id}")
            };
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());
            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;
            await _httpClient.SendAsync(request);
        }

        public Task UpdateTankAsync(int id, Tank tank, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
