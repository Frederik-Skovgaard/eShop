using Blazored.Modal;
using Blazored.Toast;
using BlazorWebShop;
using BlazorWebShop.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();

builder.Services.AddScoped<IProductAPI, ProductServiceAPI>();
builder.Services.AddScoped<ITypesAPI, TypesServiceAPI>();

builder.Services.AddHttpClient<IProductAPI, ProductServiceAPI>(config => config.BaseAddress = AppConfig.apiUrl);
builder.Services.AddHttpClient<ITypesAPI, TypesServiceAPI>(config => config.BaseAddress = AppConfig.apiUrl);


await builder.Build().RunAsync();
