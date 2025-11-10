using System.Text;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Common.Interfaces;
using AdministrationSystem.Infrastructure.Authentication.PasswordHasher;
using AdministrationSystem.Infrastructure.Authentication.TokenGenerator;
using AdministrationSystem.Infrastructure.Common.Persistance;
using AdministrationSystem.Infrastructure.Common.Storage;
using AdministrationSystem.Infrastructure.Finances;
using AdministrationSystem.Infrastructure.Products;
using AdministrationSystem.Infrastructure.Sales;
using AdministrationSystem.Infrastructure.Sites;
using AdministrationSystem.Infrastructure.Users;
using AdministrationSystem.Infrastructure.WebSites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdministrationSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddHttpClient<IRetellClient, RetellClient>(client =>
        //{
        //    client.BaseAddress = new Uri("https://api.retellai.com/");
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //});
            
        return services
            .AddAuthentication(configuration)
            .AddPersistence(configuration);
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               configuration.GetConnectionString("DevelopmentConnection");
                               
        services.AddDbContext<AdministrationSystemDBContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 0))));

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AdministrationSystemDBContext>());
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ISitesRepository, SitesRepository>();
        services.AddScoped<IWebSiteRepository, WebSiteRepository>();
        services.AddScoped<IProductsRepository, ProductRepository>();
        services.AddScoped<ISalesRepository, SalesRepository>();
        services.AddScoped<IFinanceRepository, FinanceRepository>();
        services.AddScoped<ICloudStorageService, GoogleCloudStorageService>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),

                NameClaimType = "id",
                RoleClaimType = "role"
            });

        return services;
    }
}
