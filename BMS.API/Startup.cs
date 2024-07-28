using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using OpenIddict.Validation.AspNetCore;
using BMS.Core.Helpers;

using BMS.DTO.Response;

using BMS.Infrastructure;
using BMS.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using BMS.DTO.Common.Security;
using BMS.Core.Interfaces.Security;
using BMS.Core.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BMS.Core.Interfaces.Registration;
using BMS.Core.Service.Registration;
namespace BMS.API
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostEnvironment env)
        {
            Configuration = configuration;
            _logger = logger;
            _env = env;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Register  Localization

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #endregion

            #region Register MVC
            // Register Mvc with Response Formatters


            services.AddMvc()
                //.AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            // Register API Versioning
            // services.AddApiVersioning(o => o.ReportApiVersions = true);


            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
            });

            services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            });


            #endregion

            #region Add Response Compression

            // Configure Compression level
            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            // Add Response compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            #endregion

            #region CORS

            var cors = Configuration.GetSection("CorsURL").GetChildren().Select(x => x.Value).ToArray();

            //services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => builder.WithOrigins(cors)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true));
            });


            #endregion

            #region Register all application services (DI)

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddTransient<IClientConfigurationService, ClientConfigurationService>();

            //services.AddTransient<IUserService, UserService>();

            services.AddTransient<ICurrentSessionHelper, CurrentSessionHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRegistration, Registration>();
            #endregion



            #region Configure service with OpenIddict: Authorization Server

            services.AddDbContext<BMSContext>(options =>
            {
                // Configure the context to use an in-memory store.
                // options.UseInMemoryDatabase(nameof(DbContext));

                // To Use sql server 
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                options.UseOpenIddict();


            });




            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            #endregion
            services.AddControllers(opts =>
            {
                if (_env.IsDevelopment())
                {
                    opts.Filters.Add<AllowAnonymousFilter>();

                }
                else
                {
                    var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                    opts.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
                }


            });
            //#region Configure Swagger (Open API)
            //services.AddSwaggerGen(c =>
            //{
            //    //The generated Swagger JSON file will have these properties.
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "BMS API",
            //        Version = "v1",
            //        Description = "To access any information you have from this api gateway you have to authenticate with your client credential. If user authenticate with user credential, client token need to be overwritten."
            //    });

            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "Please enter token information. \r\n\r\n Enter access_token in the text input below.\r\n\r\nExample: \"12345abcdef\"",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            }, new List<string>()
            //        }
            //    });

            //    //Locate the XML file being generated by ASP.NET...
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.XML", SearchOption.AllDirectories))
            //    {
            //        c.IncludeXmlComments(name);
            //    }
            //});

            //#endregion
            #region === Swagger generator

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "iBOS Microservice Service"
                });

                //c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Enter the request header in the following box to add Jwt To grant authorization Token: Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            #endregion ======================= close
            #region Register Asp.net core Data Protection 
            var keyLocation = Configuration["SharedKeyLocation"];
            var appName = Configuration["ApplicationName"];
            //services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(keyLocation)).SetApplicationName(appName);


            services.AddDataProtection();

            #endregion

            #region Configure Database Connection
            var items = new List<ConnectionStringSettings>();
            items.Add(new ConnectionStringSettings { Name = "AppDB", ProviderName = "SqlServer", ConnectionString = Configuration.GetConnectionString("DBConnection") });
            DBStartup.Init(items);

            #endregion

            #region Model State Validation

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    //var res = SetResponses.SetFaildResponse("The (" + string.Join(",", context.ModelState.Keys) + ") field " + (context.ModelState.Keys.Count() == 1 ? "is" : "are") + " not valid.", context.ModelState.Select(a => new { Key = a.Key, Error = a.Value.Errors.FirstOrDefault().ErrorMessage }).ToList());
                    return new ObjectResult("The (" + string.Join(",", context.ModelState.Keys) + ") field " + (context.ModelState.Keys.Count() == 1 ? "is" : "are") + " not valid.")
                    {
                        ContentTypes = { "application/json", "application/xml" },
                        StatusCode = 400
                    };
                };
            });

            #endregion

            JwtConfiguration(services);
            // services.AddHostedService<Worker>();

        }

        private void JwtConfiguration(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            services.Configure<Audience>(Configuration.GetSection("Audience"));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_env.IsProduction() ? Configuration.GetSection("REACT_APP_SECRET_NAME").Value.Trim() : audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "AuthScheme";
            })
            .AddJwtBearer("AuthScheme", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Using Logging

            //app.UseRequestResponseLogging();

            #endregion

            #region Use Response Compression

            app.UseResponseCompression();

            #endregion

            #region CORS

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            #endregion

            #region Other Setting

            app.UseSwagger();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "bms/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/bms/swagger/v1/swagger.json", "BMS");
                c.RoutePrefix = "swagger";
            });

            #endregion

            #region Localization 

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("bn-BD"),
                new CultureInfo("en-US"),

            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            #endregion

            #region Authentican , Authorization, Routing & SignalR

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            #endregion

        }
    }
}
