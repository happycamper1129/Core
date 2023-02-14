﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AspNetCoreSpa.Application.Abstractions;
using AspNetCoreSpa.Application.Extensions;
using AspNetCoreSpa.Application.Settings;
using AspNetCoreSpa.Common;
using AspNetCoreSpa.Infrastructure.Identity;
using AspNetCoreSpa.Infrastructure.Identity.Entities;
using AspNetCoreSpa.Infrastructure.Localization;
using AspNetCoreSpa.Infrastructure.Localization.EFLocalizer;
using AspNetCoreSpa.Infrastructure.Services;
using AspNetCoreSpa.Infrastructure.Services.Certificate;
using AspNetCoreSpa.Infrastructure.Services.Email;
using AspNetCoreSpa.Persistence;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace AspNetCoreSpa.Infrastructure
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddHttpContextAccessor()
                .AddResponseCompression()
                .AddMemoryCache()
                .AddHealthChecks()
                .AddDbContextCheck<LocalizationDbContext>()
                .AddDbContextCheck<IdentityServerDbContext>();

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(Constants.DefaultCorsPolicy,
                        builder =>
                        {
                            builder.WithOrigins(configuration["CorsOrigins"].Split(","))
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });

            return services;
        }

        public static IServiceCollection AddCustomSignalR(this IServiceCollection services)
        {
            services.AddSignalR()
                .AddMessagePackProtocol();

            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("de-DE"),
                        new CultureInfo("de-CH"),
                        new CultureInfo("it-IT"),
                        new CultureInfo("gsw-CH"),
                        new CultureInfo("fr-FR")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "de-DE", uiCulture: "de-DE");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            return services;
        }
        public static IIdentityServerBuilder AddCustomIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddTransient<IClientInfoService, ClientInfoService>();

            services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<IdentityServerDbContext>();

            var x509Certificate2 = GetCertificate(environment, configuration);

            var identityBuilder = services.AddIdentityServer()
                .AddSigningCredential(x509Certificate2)
                .AddApiAuthorization<ApplicationUser, IdentityServerDbContext>();

            // TODO: config
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "476611152863-ltgqfk9jhq1vsenin5039n58ogkraltb.apps.googleusercontent.com";
                    options.ClientSecret = "rSHvhgdOQUB4KMc5JS1alzhg";
                })
                .AddOpenIdConnect("aad", "Login with Azure AD", options =>
                {
                    options.Authority = $"https://login.microsoftonline.com/common";
                    options.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = false };
                    options.ClientId = "99eb0b9d-ca40-476e-b5ac-6f4c32bfb530";
                    options.CallbackPath = "/signin-oidc";
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                })
                .AddIdentityServerJwt();

            return identityBuilder;
        }

        public static IServiceCollection AddIdentityDb(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<IdentityServerDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Identity"),
                    b => b.MigrationsAssembly(typeof(IdentityServerDbContext).Assembly.FullName));

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                bool.TryParse(configuration["Data:useSqLite"], out var useSqlite);
                bool.TryParse(configuration["Data:useInMemory"], out var useInMemory);
                var connectionString = configuration["Data:Web"];

                if (useInMemory)
                {
                    options.UseInMemoryDatabase(nameof(AspNetCoreSpa)); // Takes database name
                }
                else if (useSqlite)
                {
                    options.UseSqlite(connectionString, b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        //b.UseNetTopologySuite();
                    });
                }
                else
                {
                    options.UseSqlServer(connectionString, b =>
                    {
                        b.MigrationsAssembly("AspNetCoreSpa.Infrastructure");
                        // Add foolowing package to enable net topology suite for sql server:
                        // <PackageReference Include = "Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite" Version = "2.2.0" />
                        //b.UseNetTopologySuite();
                    });
                }
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddDbLocalization(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<LocalizationDbContext>(options =>
                {
                    options.UseSqlite(configuration["DatA:Localization"],
                        b => b.MigrationsAssembly(typeof(LocalizationDbContext).Assembly.FullName));

                    if (environment.IsDevelopment())
                    {
                        options.EnableSensitiveDataLogging();
                    }
                },
            ServiceLifetime.Singleton,
            ServiceLifetime.Singleton);

            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            services.AddSingleton<ILocalizationDbContext>(provider => provider.GetService<LocalizationDbContext>());

            return services;
        }

        public static IServiceCollection AddCustomUi(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "AspNetCoreSpa API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var controllerWithViews = services.AddControllersWithViews();
            var razorPages = services.AddRazorPages()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

            if (environment.IsDevelopment())
            {
                controllerWithViews.AddRazorRuntimeCompilation();
                razorPages.AddRazorRuntimeCompilation();
            }

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Custom configuration
            services.ConfigurePoco<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

            return services;
        }

        private static X509Certificate2 GetCertificate(IWebHostEnvironment environment, IConfiguration configuration)
        {
            var useDevCertificate = bool.Parse(configuration["UseDevCertificate"]);

            var cert = new X509Certificate2(Path.Combine(environment.ContentRootPath, "sts_dev_cert.pfx"), "1234");

            if (environment.IsProduction() && !useDevCertificate)
            {
                var useLocalCertStore = Convert.ToBoolean(configuration["UseLocalCertStore"]);

                if (useLocalCertStore)
                {
                    using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    var certificateThumbprint = configuration["CertificateThumbprint"];

                    store.Open(OpenFlags.ReadOnly);
                    var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
                    cert = certs[0];
                    store.Close();
                }
                else
                {
                    // Azure deployment, will be used if deployed to Azure
                    var vaultConfigSection = configuration.GetSection("Vault");
                    var keyVaultService = new KeyVaultCertificateService(vaultConfigSection["Url"], vaultConfigSection["ClientId"], vaultConfigSection["ClientSecret"]);
                    cert = keyVaultService.GetCertificateFromKeyVault(vaultConfigSection["CertificateName"]);
                }
            }
            return cert;
        }

    }
}
