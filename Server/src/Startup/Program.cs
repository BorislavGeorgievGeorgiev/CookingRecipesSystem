using CookingRecipesSystem.Application;
using CookingRecipesSystem.Infrastructure;
using CookingRecipesSystem.Infrastructure.Persistence.Initialize;
using CookingRecipesSystem.Startup;
using CookingRecipesSystem.Web;
using CookingRecipesSystem.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebComponents();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

const string MyAllowTestOrigins = "_myAllowTestOrigins";
const string MyAllowProductionOrigins = "_myAllowProductionOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(
		MyAllowTestOrigins,
		policy =>
		{
			policy.WithOrigins("https://localhost:7072", "http://localhost:5072",
				"https://localhost", "http://localhost")
			.AllowAnyHeader();
		});
	options.AddPolicy(
		MyAllowProductionOrigins,
		policy =>
		{
			policy.WithOrigins("https://localhost", "http://localhost")
			.AllowAnyHeader();
		});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

// =================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	app.UseCors(MyAllowTestOrigins);

	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	await DataSeeder.SeedAsync(services);
}

if (app.Environment.IsProduction())
{
	app.UseCors(MyAllowProductionOrigins);
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();