using Blazored.LocalStorage;

using CookingRecipesSystem.Startup;
using CookingRecipesSystem.Startup.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddOptions();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddScoped<AuthStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(
//	s => s.GetRequiredService<AuthStateProvider>());
//builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
