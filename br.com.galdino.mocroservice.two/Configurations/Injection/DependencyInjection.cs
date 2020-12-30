using br.com.galdino.mocroservice.two.api.Filters.Performace;
using br.com.galdino.mocroservice.two.application.Interface;
using br.com.galdino.mocroservice.two.application.Service;
using br.com.galdino.mocroservice.two.domain.core.Interface;
using br.com.galdino.mocroservice.two.domain.core.Service;
using br.com.galdino.mocroservice.two.infra.data.Interface;
using br.com.galdino.mocroservice.two.infra.data.Service;
using Microsoft.Extensions.DependencyInjection;

namespace br.com.galdino.mocroservice.two.api.Configurations.Injection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {

            service.AddScoped<ICalculateSquareValueRepository, CalculateSquareValueRepository>();

            service.AddScoped<ICalculateSquareValueAppService, CalculateSquareValueAppService>();

            service.AddScoped<ICalculateSquareValueService, CalculateSquareValueService>();

            service.AddTransient<PerformaceFilters>();

            return service;
        }
    }
}
