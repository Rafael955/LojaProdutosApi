using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProdutosApp.UI;
using ProdutosApp.UI.Configurations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Ativando a biblioteca Blazored Session Storage
builder.Services.AddBlazoredSessionStorage();

//Configuração para UI do Blazor conectar com a API de Users
builder.Services.AddHttpClient(HttpClientName.UsersAPI, (_, c) =>
{
    c.BaseAddress = new Uri("http://localhost:5249");
});

builder.Services.AddHttpClient(HttpClientName.ProductsAPI, (_, c) =>
{
    c.BaseAddress = new Uri("http://localhost:5010");
});

await builder.Build().RunAsync();
