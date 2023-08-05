using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WS.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api de Autenticação de Micro Serviços",
                        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                        Description = "EFCore 5.0.1 - Api geral de autenticação de micro serviços."
                    });
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); // ignorar conflito de rotas nas controllers(não usar)
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = configuration.GetSection("securityDefinitions:Bearer:Scheme").Value,
                    BearerFormat = configuration.GetSection("securityDefinitions:Bearer:BearerFormat").Value,
                    Name = configuration.GetSection("securityDefinitions:Bearer:Name").Value,
                    In = (ParameterLocation)Enum.Parse(typeof(ParameterLocation),configuration.GetSection("securityDefinitions:Bearer:In").Value),
                    Type = (SecuritySchemeType)Enum.Parse(typeof(SecuritySchemeType), configuration.GetSection("securityDefinitions:Bearer:Type").Value),
                    Description = configuration.GetSection("securityDefinitions:Bearer:Description").Value,

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

                c.AddFluentValidationRules();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "WS.Core.Shared.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("swagger/v1/swagger.json", "WS v1");
                //c.SwaggerEndpoint("./v1/swagger.json", "WS V1");
            });
        }
    }
}
