using System.Reflection;
using FluentValidation;
using MyFoodTracker.Data.AzureBlobStorage;
using MyFoodTracker.Data.FileSystem;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") 
{
   builder.Services.Configure<MyFoodTrackerSettings>(builder.Configuration.GetSection(MyFoodTrackerSettings.Name));
   builder.Services.AddJsonFoodRepository(builder.Configuration);
}
else // Production
{
   builder.Services.AddAzureBlobFoodRepository();
}

builder.Services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
