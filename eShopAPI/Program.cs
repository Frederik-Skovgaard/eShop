using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using Microsoft.OpenApi.Models;
using System.Security.Policy;
using System.Reflection;
using eShopAPI.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the containe>
builder.Services.AddScoped<IProduct, ProductService>();

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new VcardInputFormatter());
    options.OutputFormatters.Insert(0, new VcardOutputFormatter());
});

builder.Services.AddDbContext<eShopContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("Context") ?? throw new InvalidOperationException("Connection string 'Context' not found")));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "eShopAPI",
        Description = "This is a api used for crud",
        TermsOfService = new Uri("https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.google.com")
        },

    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
