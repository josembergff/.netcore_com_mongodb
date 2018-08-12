using Microsoft.Extensions.DependencyInjection;
using MongoApi.Data;

namespace MongoApi.IoC {
    public static class DependencyInjection {
        public static void UseLojaInformaticaDependencies (this IServiceCollection services) {
            services.AddTransient<Context> ();
            services.AddScoped<IRepository, RepositoryMongo> ();
        }
    }
}