using System.Reflection;
using FluentValidation;
using MyFoodTracker.Data.AzureBlobStorage;
using MyFoodTracker.Data.FileSystem;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production") 
{
   builder.Services.AddAzureBlobFoodRepository();
}
else // Default use local files 
{
   builder.Services.Configure<MyFoodTrackerSettings>(builder.Configuration.GetSection(MyFoodTrackerSettings.Name));
   builder.Services.AddJsonFoodRepository(builder.Configuration);
}

const string CorsPolicyDefault = "default";
builder.Services.AddCors(options =>
{
   options.AddPolicy(name: CorsPolicyDefault,
      policy  =>
      {
         policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
      });
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors(CorsPolicyDefault);
app.UseAuthorization();

app.MapControllers();

app.Run();
