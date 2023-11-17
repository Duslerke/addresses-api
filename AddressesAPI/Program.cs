using AddressesAPI.v1.Gateways;
using AddressesAPI.v1.Infrastructure;
using AddressesAPI.v1.Usecases;
using AddressesAPI.v1.Validators;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        builder.Services.AddDbContext<AddressesContext>(opt => opt.UseNpgsql(connectionString));
        builder.Services.AddTransient<IAddressGateway, AddressGateway>();
        builder.Services.AddTransient<IGetAddressesByPostcodeUseCase, GetAddressesByPostcodeUseCase>();
        builder.Services.AddTransient<IGetAddressByPostcodeValidator, GetAddressByPostcodeValidator>();

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
    }
}
