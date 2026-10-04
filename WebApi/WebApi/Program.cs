using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Domain.Repositories;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>( options =>
{
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=hotels;Trusted_Connection=True;TrustServerCertificate=True;",
        options => options.MigrationsAssembly( "Infrastructure.Migrations" ) );
} );

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<PropertyService, PropertyService>();
builder.Services.AddScoped<RoomTypeService, RoomTypeService>();
builder.Services.AddScoped<ReservationService, ReservationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.MapOpenApi();
    app.UseSwagger( c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    } );
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
