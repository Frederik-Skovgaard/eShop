using BlazorWebShop.Models;

namespace BlazorWebShop.Services
{
    public interface ITypesAPI
    {
        Task<List<Types>?> GetTypesAsync();
    }
}
