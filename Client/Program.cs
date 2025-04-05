using CNHephaestus;
using CNHephaestus.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ProxyService>();
builder.Services.AddScoped<CNHSystemService>();

builder.Services.AddScoped(sp => new HttpClient
 {
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
 }
);

await builder.Build().RunAsync();
