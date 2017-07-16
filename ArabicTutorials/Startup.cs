using System;
using System.IO;
using ArabicTutorials.Bootstrap;
using ArabicTutorials.Common;
using ArabicTutorials.Common.Logger;
using AspNetCore.Identity.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ArabicTutorials.Common.Config;
using ArabicTutorials.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ILogger = ArabicTutorials.Common.ILogger;

namespace ArabicTutorials
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        private const string ApiSettingSectionKey = "ApiSettings";
        private const string FacebookKeys = "FacebookKeys";

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
            services.Configure<FacebookAuthKeys>(Configuration.GetSection(FacebookKeys));
            services.AddSingleton<IUserStore<User>>(provider =>
            {
                var options = provider.GetService<IOptions<MongoDbSettings>>();
                var client = new MongoClient(options.Value.ConnectionString);
                var database = client.GetDatabase(options.Value.DatabaseName);
                return new MongoUserStore<User>(database);
            });

            services.Configure<IdentityOptions>(options =>
            {
                var dataProtectionPath = Path.Combine(_env.WebRootPath, "identity-artifacts");
                options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
                options.Cookies.ApplicationCookie.DataProtectionProvider = DataProtectionProvider.Create(dataProtectionPath);
                options.Lockout.AllowedForNewUsers = true;

            });

            services.AddMvc(options =>
            {
                options.SslPort = 44321;
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddAuthentication(options =>
            {
                options.SignInScheme = new IdentityCookieOptions().ExternalCookieAuthenticationScheme;
            });

            services.AddOptions();

            services.AddDataProtection();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>();

            AddDefaultTokenProviders(services);

            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>();
            var apiSettings = Configuration
              .GetSection(ApiSettingSectionKey).Get<Settings>();
            builder.RegisterType<HttpContextAccessor>().AsSelf().SingleInstance();
            builder.RegisterInstance(apiSettings).AsSelf();
            builder.RegisterModule<IdentityModule>();
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        private void AddDefaultTokenProviders(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<>).MakeGenericType(typeof(User));
            AddTokenProvider(services, TokenOptions.DefaultProvider, dataProtectionProviderType);
        }

        private void AddTokenProvider(IServiceCollection services, string providerName, Type provider)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.ProviderMap[providerName] = new TokenProviderDescriptor(provider);
            });

            services.AddSingleton(provider);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseIdentityServer();

            var facebookKeys = Configuration
               .GetSection(FacebookKeys).Get<FacebookAuthKeys>();
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AppId = facebookKeys.ApplicationId,
                AppSecret = facebookKeys.ApplicationSecret
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
