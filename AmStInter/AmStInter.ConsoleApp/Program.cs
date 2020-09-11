using AmStInter.Application.Orders.Commands;
using AmStInter.Application.Orders.Commands.UpdateStock;
using AmStInter.Application.Orders.Queries.GetInProgressOrders;
using AmStInter.Application.Orders.Queries.GetTopSoldProducts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AmStInter.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = AppConfiguration.BuildConfiguration();
            var serviceProvider = DependencyInjectionSetup.Register(configuration);

            var mediator = serviceProvider.GetService<IMediator>();

            await DisplayOrders(mediator);
            Console.WriteLine();
            await DisplayTopSoldProducts(mediator);
            Console.WriteLine();
            await DisplayUpdateProductStock(mediator);

            Console.ReadKey();
        }

        static async Task DisplayOrders(IMediator mediator)
        {
            Console.WriteLine("In progres orders:");
            var orders = await mediator.Send(new GetInProgressOrdersQuery());

            foreach (var order in orders)
            {
                Console.WriteLine($"Id: {order.Id}, Status: {order.Status}, ChannelName: {order.ChannelName}");
            }
        }

        static async Task DisplayTopSoldProducts(IMediator mediator)
        {
            Console.WriteLine("Top 5 products:");
            var products = await mediator.Send(new GetTopSoldProductsQuery());

            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Total Quantity: {product.TotalQuantity}, EAN: {product.EAN}, MerchantProductNo: {product.MerchantProductNo}");
            }
        }

        static async Task DisplayUpdateProductStock(IMediator mediator)
        {
            Console.WriteLine("Type merchant product to update:");
            var merchantNumber = Console.ReadLine();
            try
            {
                await mediator.Send(new UpdateStockCommand(merchantNumber));
            }
            catch (UpdatedMerchantProductNoDoesNotExistException)
            {
                Console.WriteLine("Product not exist!");
                return;
            }

            Console.WriteLine("Product updated!");
        }
    }
}
