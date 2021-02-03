using System;
using System.IO;
using System.Text;
using DaoLibrary.Entities;
using DaoLibrary.Repositories;
using EntityGeneratorWebApp.Config;
using EntityGeneratorWebApp.Models;
using EntityGeneratorWebApp.Utils;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace EntityGeneratorWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //加载log4net日志配置文件
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
        }

        public static ILoggerRepository LoggerRepository { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            // 面向Action切面捕获全局异常
            services.AddControllers(configure => { configure.Filters.Add<HttpGlobalExceptionFilter>(); });
            // 注入Configuration对象
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"))
                .Configure<RsaKeyPair>(Configuration.GetSection("RsaKeyPair"));
            // 注入业务模块
            services.AddTransient<ISqlServerRepository, SqlServerRepository>();
            services.AddTransient<IMySqlRepository, MySqlRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();

            // 注册Swagger生成器
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Entity Generator Asp.Net Core WebApi",
                    Version = "v1",
                    Description = "数据库实体类生成器WebApi",
                    Contact = new OpenApiContact
                    {
                        Name = "谭武成",
                        Email = "tanwucheng@outlook.com",
                        Url = new Uri("https://github.com/tanwucheng")
                    }
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "EntityGeneratorWebApp.xml");
                options.IncludeXmlComments(xmlPath);

                var scheme = new OpenApiSecurityScheme
                {
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Bearer Token"
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });

            // 添加jwt验证：
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //是否验证Issuer
                        ValidateAudience = true, //是否验证Audience
                        ValidateLifetime = true, //是否验证失效时间
                        ValidateIssuerSigningKey = true, //是否验证SecurityKey
                        ValidAudience = "ten", //Audience
                        ValidIssuer = "谭武成", //Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("encrypt_the_validate_site_key")) //拿到SecurityKey
                    };
                });

            services.AddMvc()
                .AddMvcLocalization()
                .AddViewLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 请求错误提示配置
            app.UseErrorHandling();

            // 启用验证
            app.UseAuthentication();
            app.UseAuthorization();

            // 插入中间件以将生成的Swagger公开为JSON端点
            app.UseSwagger();

            // 如果您想公开交互式文档，可以选择插入Swagger-ui中间件，并指定用于支持它的Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Entity Generator Asp.Net Core WebApi V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("encrypt_the_validate_site_key"));
            var options = new TokenGenerateOption
            {
                Path = "/token",
                Audience = "ten",
                Issuer = "谭武成",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(7)
            };

            var userAuthRepository = app.ApplicationServices.GetService<IAuthRepository>();
            var keyPairs = Configuration.GetSection("RsaKeyPair").GetChildren();
            var privateKey = "";
            foreach (var item in keyPairs)
            {
                if (item.Key != "PrivateKey") continue;
                privateKey = item.Value;
                break;
            }
            var tokenGenerator = new TokenGenerator(options, userAuthRepository, privateKey);
            app.Map(options.Path, tokenGenerator.GenerateTokenAsync);
        }
    }
}