using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Text.Json.Serialization;
using WS.Mananger.Validator.Alterar;
using WS.Mananger.Validator.Novo;

namespace WS.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddJsonOptions(p =>
            {
                p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<AspNetClientNovoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AspNetClientAlterarValidator>();

                p.RegisterValidatorsFromAssemblyContaining<AspNetModuleNovoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AspNetModuleAlterarValidator>();

                p.RegisterValidatorsFromAssemblyContaining<AspNetMenuNovoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AspNetMenuAlterarValidator>();

                p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            });
        }
    }
}
