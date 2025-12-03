using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public static class DataDependency
{
    /// <summary>
    /// The Dependency Injection for the data access layer.
    /// </summary>
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString,
                             ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IteachersRepository, teachersRepository>();
        services.AddScoped<IstudentsRepository, studentsRepository>();
        services.AddScoped<IclassesRepository, classesRepository>();
        services.AddScoped<Iclass_enrollmentsRepository, class_enrollmentsRepository>();
        services.AddScoped<IgradesRepository, gradesRepository>();
    }
}