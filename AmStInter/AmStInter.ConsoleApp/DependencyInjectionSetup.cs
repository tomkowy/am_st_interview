using AmStInter.Application.Orders.Queries.GetInProgressOrders;
using AmStInter.Application.Orders.Services;
using AmStInter.DataSource.DataSources;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AmStInter.ConsoleApp
{
    public static class DependencyInjectionSetup
    {
        public static ServiceProvider Register(IConfiguration configuration)
        {
            var collection = new ServiceCollection();

            var dataSourceSection = configuration.GetSection("DataSource");
            var apiUrl = dataSourceSection["ApiUrl"];
            var apiKey = dataSourceSection["ApiKey"];

            collection.AddScoped<ITopSoldProductService, TopSoldProductService>();
            collection.AddScoped<IDataSource>(
                x => new ChannelEngineDataSource(apiUrl, apiKey));
            collection.AddMediatR(typeof(GetInProgressOrdersQuery).GetTypeInfo().Assembly);

            var serviceProvider = collection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
