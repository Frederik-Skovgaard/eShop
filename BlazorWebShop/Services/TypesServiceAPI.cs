﻿using BlazorWebShop.Models;
using System.Net.Http.Json;

namespace BlazorWebShop.Services
{
    public class TypesServiceAPI : ITypesAPI
    {
        private readonly HttpClient _httpClient;

        public TypesServiceAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Types> GetTypeAsync(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<Types>($"/api/Type/item/{id}");

            if (item != null)
            {
                return item;
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<List<Types>?> GetTypesAsync() => await _httpClient.GetFromJsonAsync<List<Types>>("/api/Type");
    }
}
