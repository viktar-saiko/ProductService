using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Data.MongoDb.Extensions;
using Common.Middleware;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Switcher between MongoDB and PostgreSQL
            bool useMongoDb = builder.Configuration.GetValue<bool>("UseMongoDB");
            if (useMongoDb)
            {
                builder.Services.AddScoped<IProductRepository, Data.MongoDb.Repositories.ProductRepository>();

                var mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings");
                if (mongoDbSettings == null)
                {
                    throw new ArgumentNullException("Parameter MongoDBSettings not specified");
                }

                builder.Services.Configure<Data.MongoDb.Configurations.DatabaseSettings>(mongoDbSettings);
                builder.Services.RegisterMongoClient(mongoDbSettings);

            }
            else
            {
                builder.Services.AddScoped<IProductRepository, Data.Sql.Repositories.ProductRepository>();
                //builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

                string postgresDb = builder.Configuration.GetConnectionString("PostgresDBConnection") ?? string.Empty;
                if (postgresDb == null)
                {
                    throw new ArgumentNullException("Parameter PostgresDBConnection not specified");
                }

                //builder.Services.AddDbContextPool<Data.Sql.Repositories.AppDbContext>(t => t.UseNpgsql(postgresDb));
                builder.Services.AddDbContext<Data.Sql.Repositories.AppDbContext>(t => t.UseNpgsql(postgresDb));
            }

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(t =>
            {
                t.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ProductServiceApi", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // Allows access only from ApiGateway.
                app.UseMiddleware<RestrictAccessMiddleware>();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
