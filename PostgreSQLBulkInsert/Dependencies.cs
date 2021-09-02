using LinqToDB.AspNet;
using LinqToDB.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PostgreSQLBulkInsert
{
    public class Dependencies
    {
        private readonly ServiceProvider _serviceProvider;

        public const string PostgresConnectionString = "Host=localhost;Database=test_db;Username=pgUser;Password=pgPassword";

        public Dependencies()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(PostgresConnectionString));

            services.AddLinqToDbContext<PostgresDataConnectionForLinq2Db>((provider, options) =>
            {
                options.UsePostgreSQL(PostgresConnectionString);
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}