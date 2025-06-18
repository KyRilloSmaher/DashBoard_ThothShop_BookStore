using System.Text.Json;
using ThothStore_DashBoard.Models.Bases;

namespace ThothStore_DashBoard.Services.Interfaces
{
    public interface IAPIService
    {
        Task<Response<T>> GetAsync<T>(string endpoint);
        Task<PaginatedResponse<T>> GetAsyncPaginated<T>(string endpoint);
        Task<Response<T>> PostAsync<T>(string endpoint, object data ,bool useFormData = false);
        Task<Response<T>> PutAsync<T>(string endpoint, object data, bool useFormData = false);
        Task<bool> DeleteAsync(string endpoint);
       
    }
}
