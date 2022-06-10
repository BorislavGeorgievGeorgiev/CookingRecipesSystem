using CookingRecipesSystem.Application;
using CookingRecipesSystem.Infrastructure;
using CookingRecipesSystem.Infrastructure.Persistence.Initialize;
using CookingRecipesSystem.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebComponents();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	await DataSeeder.SeedAsync(services);
}

app.UseHttpsRedirection();

app.Run();
