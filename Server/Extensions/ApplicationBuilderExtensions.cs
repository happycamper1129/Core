﻿using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders;

namespace AspNetCoreSpa.Server.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomisedHeadersMiddleware(this IApplicationBuilder app)
        {
            var policyCollection = new HeaderPolicyCollection()
                   .AddFrameOptionsDeny()
                   .AddXssProtectionBlock()
                   .AddContentTypeOptionsNoSniff()
                   .AddStrictTransportSecurityMaxAge(maxAge: 60 * 60 * 24 * 365) // maxage = one year in seconds
                   .AddReferrerPolicyOriginWhenCrossOrigin()
                   .RemoveServerHeader();
                //    .AddCustomHeader("X-My-Test-Header", "Header value");

            app.UseCustomHeadersMiddleware(policyCollection);
            return app;
        }
        public static IApplicationBuilder UseCustomWebpackDevMiddleware(this IApplicationBuilder app)
        {
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
                HotModuleReplacement = true
            });
            return app;
        }
        public static IApplicationBuilder UseCustomSwaggerApi(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

            return app;
        }
        public static IApplicationBuilder AddDevMiddlewares(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Startup.Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseCustomWebpackDevMiddleware();
                // NOTE: For SPA swagger needs adding before MVC
                app.UseCustomSwaggerApi();
            }

            // TODO loggerFactory.AddSerilog();

            return app;
        }

        public static IApplicationBuilder AddCustomLocalization(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            return app;
        }

        public static IApplicationBuilder SetupMigrations(this IApplicationBuilder app)
        {
            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            try
            {
                var context = app.ApplicationServices.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }
            catch (Exception) { }
            return app;
        }

    }
}
