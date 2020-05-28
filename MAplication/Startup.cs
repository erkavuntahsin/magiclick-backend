using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MAplication.Data;
using MAplication.Services;
using Microsoft.Extensions.Logging;
using Core.Logger;
using BusinessLayer;
using Interfaces.Service;
using Services;
using NLog.Extensions.Logging;
using NLog.Web;
using StructureMap;
using Interfaces.Business;
using StructureMap.DynamicInterception;
using CacheLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MAplication
{
    public class Startup
    {
        ILogger<LogInterceptor> _logger;
        public Startup(IConfiguration configuration, ILogger<LogInterceptor> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options => { })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "worldenderaatrox.com",
                    ValidateIssuer = true,
                    ValidIssuer = "worldenderaatrox.com",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("worldenderaatrox00"))
                };
            });

            MapperBL.Initialize();
            var container = ContainerConfigurator.Configure(services, _logger);
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
    public class ContainerConfigurator
    {
        public static Container Configure(IServiceCollection services, ILogger<LogInterceptor> logger)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.For<ICategoryBL>().Use<CategoryBL>().Singleton().AddInterceptor(new DynamicProxyInterceptor<ICategoryBL>(new IInterceptionBehavior[] { new LogInterceptor(logger), new CachingInterceptor(), new CacheInvalidateInterceptor() }));
                config.For<IProductBL>().Use<ProductBL>().Singleton().AddInterceptor(new DynamicProxyInterceptor<IProductBL>(new IInterceptionBehavior[] { new LogInterceptor(logger), new CachingInterceptor(), new CacheInvalidateInterceptor() }));
               config.Populate(services);

            });

            return container;
        }
    }
}
