using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var edmModel = ODataModels.GetEdmModel();

builder.Services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists"));
builder.Services.AddControllers().AddOData(options => options
                                            .AddRouteComponents("odata", edmModel)
                                            .Select()
                                            .Filter()
                                            .Expand()
                                           );
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

app.UseAuthorization();

app.MapControllers();

app.Run();

internal class ODataModels
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

        builder.EntitySet<Book>("Book");
        builder.EntitySet<Press>("Press");

        return builder.GetEdmModel();
    }
}