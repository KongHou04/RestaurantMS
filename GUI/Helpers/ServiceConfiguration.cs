using BLL.Interfaces;
using BLL.Services;
using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace RM_Project1.Helpers
{
    public class ServiceConfiguration
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();

        public static ServiceProvider GetLoginServiceProvider()
        {
            var connString = configuration.GetConnectionString("PostgreSqlConnString");

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ImageHandlerSVC>(provider =>
            {
                var b = GetImageHandler();
                if (b != null)
                    return b;
                else
                    return new ImageHandlerSVC(@"C:\Users\Admin\Desktop", @"C:\Users\Admin\Desktop");
            });
            serviceCollection.AddDbContext<RMContext>(b =>
            {
                b.UseNpgsql(connString);
            });
            serviceCollection.AddSingleton<IEmployeeRES, EmployeeRES>();
            serviceCollection.AddSingleton<IAccountRES, AccountRES>();
            serviceCollection.AddSingleton<IRoleRES, RoleRES>();
            serviceCollection.AddSingleton<IUserSVC, UserSVC>();

            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider GetApplicationServiceProvider(string role)
        {
            var connString = configuration.GetConnectionString("PostgreSqlConnString");

            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ImageHandlerSVC>(provider =>
            {
                var b = GetImageHandler();
                if (b != null)
                    return b;
                else
                    return new ImageHandlerSVC(@"C:\Users\Admin\Desktop", @"C:\Users\Admin\Desktop");
            });

            serviceCollection.AddSingleton<IAreaRES, AreaRES>();
            serviceCollection.AddSingleton<ITableRES, TableRES>();
            serviceCollection.AddSingleton<ICategoryRES, CategoryRES>();
            serviceCollection.AddSingleton<IProductRES, ProductRES>();
            serviceCollection.AddSingleton<IEmployeeRES, EmployeeRES>();
            serviceCollection.AddSingleton<IAccountRES, AccountRES>();
            serviceCollection.AddSingleton<IRoleRES, RoleRES>();
            serviceCollection.AddSingleton<IOrderRES, OrderRES>();
            serviceCollection.AddSingleton<IOrderDetailRES, OrderDetailRES>();

            serviceCollection.AddDbContext<RMContext>(b =>
            {
                b.UseNpgsql(connString);
            });
            serviceCollection.AddSingleton<IAreaSVC, AreaSVC>();
            serviceCollection.AddSingleton<ICategorySVC, CategorySVC>();
            serviceCollection.AddSingleton<IProductSVC, ProductSVC>();
            serviceCollection.AddSingleton<IEmployeeSVC, EmployeeSVC>();
            serviceCollection.AddSingleton<ISelfUserSVC, SelfUserSVC>();
            serviceCollection.AddSingleton<IOrderSVC, OrderSVC>();


            return serviceCollection.BuildServiceProvider();
        }

        public static ImageHandlerSVC? GetImageHandler()
        {
            var root1 = configuration.GetSection("ImagePaths:DefaultEmployeeImageRoot");
            var root2 = configuration.GetSection("ImagePaths:DefaultProductImageRoot");
            if (root1.Value != null && root2.Value != null)
                return new ImageHandlerSVC(root1.Value, root2.Value);
            else 
                return null;
        }

    }
}
