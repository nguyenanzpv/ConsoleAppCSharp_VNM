using Microsoft.AspNetCore.Mvc.Formatters;//dung de dinh dang
using SolidEdu.Shared;//su dung model va entity
using static System.Console;
using Ecommerce.WebApi.Repositories;
using Microsoft.OpenApi.Models;//for swagger
using Swashbuckle.AspNetCore.SwaggerUI;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add method AddEcommerceContext()  to connect SQL Server
builder.Services.AddEcommerceContext();

//config allow cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers(
    options => //add them de format dinh dang xml
    {
        WriteLine("Default output formatters:");
        foreach (IOutputFormatter formatter in options.OutputFormatters)
        {
            OutputFormatter? mediaFormatter = formatter as OutputFormatter;
            if (mediaFormatter == null)
            {
                WriteLine($" {formatter.GetType().Name}");
            }
            else // OutputFormatter class has SupportedMediaTypes
            {
               WriteLine(" {0}, Media types: {1}",
               arg0: mediaFormatter.GetType().Name,
               arg1: string.Join(", ",
               mediaFormatter.SupportedMediaTypes));             
            }
        }
    })
   .AddXmlDataContractSerializerFormatters()
   .AddXmlSerializerFormatters();    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    { Title = "Ecommerce Service API", Version = "v1" });
});

//dang ky Denpence injection cho ICustomerRepository la CustomerRepository
//Tuong tu autowired cua spring
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Ecommerce Service API Version 1");
            c.SupportedSubmitMethods(new[] {
         SubmitMethod.Get, SubmitMethod.Post,
         SubmitMethod.Put, SubmitMethod.Delete });
    });
}

//app.UseHttpsRedirection();

//use Cors
//app.UseCors("AllowAnyOrigin");
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
