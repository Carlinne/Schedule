using App.Data.Repositories.ActivityRepository;
using App.Data.Repositories.BaseRepository;
using App.Data.Repositories.Property;
using App.Services.Activity;
using App.Services.Property;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.RestfulAPI.IoC
{
    public static class ResolvedServices
    {
        private static IConfiguration _configuration;
        public static IServiceCollection AddResolvedRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            return services;
        }

        public static IServiceCollection AddResolvedServices(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IPropertyService, PropertyService>();

            // AutoMapper MapperConfiguration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
