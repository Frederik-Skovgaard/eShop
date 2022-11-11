using BlazorWebShop.Models;

namespace BlazorWebShop.Services
{
    public interface ITypesAPI
    {
        Task<List<Types>?> GetTypesAsync();
        Task<Types> GetTypeAsync(int id);
    }
}
