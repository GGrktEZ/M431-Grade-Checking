using DataAccess;
using Services.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class LogicDependency
{
    /// <summary>
    /// The Depencdency Injection for the Service layer.
    /// </summary>
    public static void AddServiceLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataAccessLayer(config);

        services.AddScoped<IteachersService, teachersService>();
        services.AddScoped<IstudentsService, studentsService>();
        services.AddScoped<IclassesService, classesService>();
        services.AddScoped<Iclass_enrollmentsService, class_enrollmentsService>();
        services.AddScoped<IgradesService, gradesService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IdepartmentsService, departmentsService>();
        services.AddScoped<IprorectorsService, prorectorsService>();
        services.AddScoped<Iteacher_prorectorsService, teacher_prorectorsService>();
        services.AddScoped<Igrade_changesService, grade_changesService>();

        // NEW
        services.AddScoped<IEmailService, EmailService>();
    }
}
