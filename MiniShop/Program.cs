﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniShop.Adapter;
using MiniShop.Models;
using MiniShop.Repository;
using MiniShop.Repository.InMemory;
using MiniShop.UseCase;
using MiniWebServer.Authentication;
using MiniWebServer.Configuration;
using MiniWebServer.HttpParser.Http11;
using MiniWebServer.HttpsRedirection;
using MiniWebServer.MiniApp;
using MiniWebServer.MiniApp.Builders;
using MiniWebServer.MiniWebServer.MimeMapping;
using MiniWebServer.Server;
using MiniWebServer.Server.Abstractions;
using MiniWebServer.Server.Abstractions.Parsers.Http11;
using MiniWebServer.Session;
using MiniWebServer.StaticFiles;
using System.Text.Json;

namespace MiniShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServerBuilder serverBuilder = new MiniWebServerBuilder();
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("minishop.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            ServerOptions serverOptions = config.Get<ServerOptions>() ?? new ServerOptions();
            serverBuilder = serverBuilder
                .UseOptions(serverOptions);

            ConfigureServerServices(serverBuilder.Services);
            SetupRepositories(serverBuilder.Services);

            IMiniApp app = BuildApp(serverBuilder.Services, 0); // or server.CreateAppBuilder(); ?
            app = MapRoutes(app);
            serverBuilder.AddHost(string.Empty, app); 

            app.MapGet("/", (context, cancellationToken) =>
            {
                context.Response.Content = new MiniWebServer.MiniApp.Content.StringContent("Hello world!");

                return Task.CompletedTask;
            });

            serverBuilder.AddHost(string.Empty, app);

            var server = serverBuilder.Build();
            server.Start();
        }

        private static IMiniApp BuildApp(IServiceCollection services, int httpsPort)
        {
            MiniAppBuilder appBuilder = new(services);

            if (httpsPort > 0)
            {
                appBuilder.UseHttpsRedirection((options) => {
                    options.HttpsPort = httpsPort;
                });
            }

            appBuilder.UseAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = CookieDefaults.AuthenticationScheme;
                }
                )
                .UseCookieAuthentication();

            appBuilder.UseSession();
            appBuilder.UseStaticFiles("wwwroot", defaultMaxAge: 3600);
            appBuilder.UseMvc();

            return appBuilder.Build();
        }

        private static IMiniApp MapRoutes(IMiniApp app)
        {
            app.MapGet("/cart", (context, cancellationToken) =>
            {
                context.Response.Content = new MiniWebServer.MiniApp.Content.StringContent("cart content");
                return Task.CompletedTask;
            });

            return app;
        }

        private static void ConfigureServerServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
            services.AddDistributedMemoryCache();

            services.AddTransient<IHttpComponentParser, ByteSequenceHttpParser>();
            services.AddTransient<IProtocolHandlerFactory, ProtocolHandlerFactory>();
            services.AddSingleton<IMimeTypeMapping>(StaticMimeMapping.Instance);

            services.AddMvcService();
            services.AddSessionService();
        }

        private static void SetupRepositories(IServiceCollection services)
        {
            var productFile = new FileInfo(Path.Combine("Data", "products.json"));
            var productRepository = new InMemoryProductRepository();
            if (productFile.Exists)
            {
                using var reader = productFile.OpenText();
                string json = reader.ReadToEnd();
                var productDbModel = JsonSerializer.Deserialize<ProductDataModel>(json);

                if (productDbModel != null && productDbModel.Products != null)
                {
                    foreach (var product in productDbModel.Products)
                    {
                        productRepository.AddProduct(product);
                    }
                }
            }

            services.AddSingleton<IProductRepository>(productRepository);
            services.AddSingleton<ICatalogService>(services => new CatalogService(services.GetRequiredService<IProductRepository>()));
        }
    }
}