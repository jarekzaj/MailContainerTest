using System.Configuration;
using MailContainerTest.Data;
using MailContainerTest.Types;
using Microsoft.Extensions.DependencyInjection;

namespace MailContainerTest.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMailTransferService, MailTransferService>();
            var containerDataStore = ContainerDataStoreFactory.CreateContainerDataStore(ConfigurationManager.AppSettings["DataStoreType"]);

            services.AddSingleton(_ => containerDataStore);
            services.AddSingleton<IMailTypeValidator, MailTypeValidator>();
        }
    }
}
