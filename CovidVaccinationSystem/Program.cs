using CovidVaccinationSystem.Data;
using CovidVaccinationSystem.FluentValidations;
using CovidVaccinationSystem.UnitConfiguration;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configure DB settings 
builder.Services.AddDbContext<DBContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

//add Service of Logging Extension
builder.Services.AddLogging(configure => configure.AddConsole()).AddTransient<UnitOfWork>();

//add Service of Fulent Validator 
builder.Services.AddControllers().AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<UnitOfWork>());

//to convert output to JSON Format
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
= Newtonsoft.Json.ReferenceLoopHandling.Ignore);  

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Covid Vaccination Web API", Version = "v1" });
});

// Injected DI 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
