using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VueMSFramework.Data;
using VueMSFramework.Helpers;

namespace VueMSFramework
{
    public class Startup
    {
        public static ApplicationDbContext context;
        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("1 run Startup");
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("2 run ConfigureServices");

            IdentityModelEventSource.ShowPII = true;  //エラーの詳細を表示し、問題を表示する



            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            //GDPR 対応のため
            //https://docs.microsoft.com/en-us/aspnet/core/security/gdpr?view=aspnetcore-2.1
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //多言語対応
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)


            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            //.AddDataAnnotationsLocalization(options => {
            //    options.DataAnnotationLocalizerProvider = (type, factory) =>
            //        factory.Create(typeof(Shared));
            //})
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = false;

                options.ClientErrorMapping[404].Link = "https://httpstatuses.com/404";
            });

            //services.AddMvc();
            //services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                //options.CookieHttpOnly = true;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // 認証の設定は ConfigureServices の中で行うようになった
            services.AddAuthentication(options =>
            {
                // JwtBearer 認証しか使わないので、
                // JwtBearer をデフォルトにする
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // configure DI for application services
            //services.AddScoped<IUserService, UserService>();


            .AddJwtBearer(options =>
            {
                //着信したトークンの意図された受信者、またはトークンがアクセスを許可
                //するリソースを表す。
                // このパラメータで指定された値がトークンの aud パラメータと一致しない
                // 場合、トークンは別のリソースにアクセスするために使用されたため、
                // 拒否される。
                //options.Audience = appSettings.SiteUrl;
                //options.Audience = Configuration.GetValue<String>("WebServer:Domain");
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateAudience = false,
                    //ValidAudience = Audience,

                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.SiteUrl,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret))
                };
            });

     

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IStringLocalizerFactory stringLocalizerFactory)
        {
            Console.WriteLine("3 run Configure");

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            //多言語対応
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ja"),
                new CultureInfo("fr"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ja"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

           // app.UseWebSockets();
            //var webSocketOptions = new WebSocketOptions()
            //{
            //    KeepAliveInterval = TimeSpan.FromSeconds(120),
            //    ReceiveBufferSize = 4 * 1024
            //};
            ////app.UseWebSockets(webSocketOptions);

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/ws")
            //    {
            //        if (context.WebSockets.IsWebSocketRequest)
            //        {
            //            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //            await Echo(context, webSocket);
            //        }
            //        else
            //        {
            //            context.Response.StatusCode = 400;
            //        }
            //    }
            //    else
            //    {
            //        await next();
            //    }

            //});
        }
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

    }
}
