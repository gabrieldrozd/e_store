using Core.Specifications;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "e_store API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments<ProductSpecParams>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.InjectStylesheet("/assets/css/swaggerStyle.css");
            });

            return app;
        }

        /// <summary>
        /// Extension method to include XML comments from another project
        /// </summary>
        /// <typeparam name="TFromType">Actual class from desired Project</typeparam>
        /// <param name="options">just extension to actual SwaggerGenOptions(nothing needed to be passed in)</param>
        private static void IncludeXmlComments<TFromType>(this SwaggerGenOptions options) 
        { 
            options.IncludeXmlComments(Path.ChangeExtension(typeof(TFromType).Assembly.Location, "xml")); 
        }
    }
}
