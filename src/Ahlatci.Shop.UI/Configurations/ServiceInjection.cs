using Ahlatci.Shop.UI.Services.Abstraction;
using Ahlatci.Shop.UI.Services.Implementation;
using Ahlatci.Shop.UI.Validators.Accounts;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Ahlatci.Shop.UI.Configurations
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IRestService, RestService>();
            
            return services;
        }
    }
}
