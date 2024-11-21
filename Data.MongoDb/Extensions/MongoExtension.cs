using Data.MongoDb.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Data.MongoDb.Extensions
{
    public static class MongoExtension
    {
        public static IServiceCollection RegisterMongoClient(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            //services.Configure<DatabaseSettings>(configurationSection);
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<IMongoClient>(sp =>
            {
                var dbSettings = sp.GetRequiredService<IDatabaseSettings>();
                var settings = MongoClientSettings.FromConnectionString(dbSettings.ConnectionString);

                return new MongoClient(settings);
            });

            return services;
        }
    }
}
