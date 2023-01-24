using Microsoft.EntityFrameworkCore;
using Vehicles.Business.AutoMapper;
using Vehicles.Data.Interfaces.v1;
using Business.Interfaces.v1;
using Business.Implementations.v1;
using Persistence.Implementations.v1;
using Persistence.Context.v1;
using Persistence.Interfaces.v1;
using Persistence.Entities.v1;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        Configure(app);

        app.Run();

    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
               .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();


        services.AddSingleton(configuration);
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        services.ConfigureAutomapper();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<ITownRepository, TownRepository>();

        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<ITownService, TownService>();
        services.AddScoped<IUserService, UserService>();
    }

    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

    }
}
