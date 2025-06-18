using System.Text.Json;
using System.Text;
using ThothStore_DashBoard.Models.Bases;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThithStore_DashBoard.Services.Implemenations
{
    public class APIServices :IAPIService
    {
        private readonly HttpClient _httpClient;

        public APIServices(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<Response<T>> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        public async Task<Response<T>> PostAsync<T>(string endpoint, object data, bool useFormData = false)
        {
            HttpResponseMessage? response = null;
            if (useFormData)
            {
                var formData = new MultipartFormDataContent();

                foreach (var prop in data.GetType().GetProperties())
                {
                    var value = prop.GetValue(data);

                    if (value == null)
                        continue;

                    // Single IFormFile (e.g., PrimaryImage)
                    if (value is IFormFile file)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        formData.Add(streamContent, prop.Name, file.FileName);
                    }
                    // List<IFormFile> (e.g., Images)
                    else if (value is IEnumerable<IFormFile> fileList)
                    {
                        foreach (var imageFile in fileList)
                        {
                            if (imageFile != null)
                            {
                                var streamContent = new StreamContent(imageFile.OpenReadStream());
                                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
                                // Important: use the same prop name (e.g., "Images") for each file
                                formData.Add(streamContent, prop.Name, imageFile.FileName);
                            }
                        }
                    }
                    else
                    {
                        formData.Add(new StringContent(value.ToString()), prop.Name);
                    }
                }

                response = await _httpClient.PostAsync(endpoint, formData);
            }
            else
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(endpoint, content);
            }

            return await HandleResponse<T>(response);
        }

        public async Task<Response<T>> PutAsync<T>(string endpoint, object data , bool useFormData =false)
        {
            HttpResponseMessage? response = null;
            if (useFormData)
            {
                var formData = new MultipartFormDataContent();

                foreach (var prop in data.GetType().GetProperties())
                {
                    var value = prop.GetValue(data);

                    if (value == null)
                        continue;

                    // Single IFormFile (e.g., PrimaryImage)
                    if (value is IFormFile file)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        formData.Add(streamContent, prop.Name, file.FileName);
                    }
                    // List<IFormFile> (e.g., Images)
                    else if (value is IEnumerable<IFormFile> fileList)
                    {
                        foreach (var imageFile in fileList)
                        {
                            if (imageFile != null)
                            {
                                var streamContent = new StreamContent(imageFile.OpenReadStream());
                                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
                                // Important: use the same prop name (e.g., "Images") for each file
                                formData.Add(streamContent, prop.Name, imageFile.FileName);
                            }
                        }
                    }
                    else
                    {
                        formData.Add(new StringContent(value.ToString()), prop.Name);
                    }
                }

                response = await _httpClient.PutAsync(endpoint, formData);
            }

            else
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                 response = await _httpClient.PutAsync(endpoint, content);
            }
            
            return await HandleResponse<T>(response);
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<Response<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var jsonSerialized = JsonSerializer.Deserialize<Response<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception("Failed to deserialize API response");

            return jsonSerialized;
        }

        public async Task<PaginatedResponse<T>> GetAsyncPaginated<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var jsonSerialized = JsonSerializer.Deserialize<PaginatedResponse<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception("Failed to deserialize API response");

            return jsonSerialized;
        }
    }
}
