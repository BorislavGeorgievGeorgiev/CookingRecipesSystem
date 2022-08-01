using CookingRecipesSystem.Application;
using CookingRecipesSystem.Infrastructure;
using CookingRecipesSystem.Infrastructure.Persistence.Initialize;
using CookingRecipesSystem.Web;
using CookingRecipesSystem.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebComponents();

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
				"https://localhost", "http://localhost");
		});
	options.AddPolicy(
		MyAllowProductionOrigins,
		policy =>
		{
			policy.WithOrigins("https://localhost", "http://localhost");
		});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
				Reference = new Microsoft.OpenApi.Models.OpenApiReference {
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

// =================================================

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseRouting();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();