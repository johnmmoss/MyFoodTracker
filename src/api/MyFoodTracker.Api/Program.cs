using System.Reflection;
using FluentValidation;
using MyFoodTracker.Api;
using MyFoodTracker.Api.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Setup:
builder.Services.AddTransient<IFoodRepository, FoodRepository>();
builder.Services.AddTransient<IFileSystem, FileSystem>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
builder.Services.Configure<MyFoodTrackerSettings>(builder.Configuration.GetSection(MyFoodTrackerSettings.Name));

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
