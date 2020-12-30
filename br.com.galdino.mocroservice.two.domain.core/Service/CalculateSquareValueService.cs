using br.com.galdino.mocroservice.two.domain.core.Interface;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity.Response;
using br.com.galdino.mocroservice.two.domain.crosscouting.Enum;
using br.com.galdino.mocroservice.two.domain.crosscouting.Infrastrscture;
using br.com.galdino.mocroservice.two.utils.Interface.Request;
using System;
using System.Threading.Tasks;

namespace br.com.galdino.mocroservice.two.domain.core.Service
{
    public class CalculateSquareValueService : ICalculateSquareValueService
    {
        private readonly IWebRequestService webRequest;

        private readonly MicroServicesConfig microServices;
        public CalculateSquareValueService(IWebRequestService webRequest, InfrastructureConfig infrastructureConfig)
        {
            this.webRequest = webRequest;
            this.microServices = infrastructureConfig.MicroServices;
        }
        public async Task<CalculateModelView> Post(decimal met)
        {
            var valueSquareService =
                await webRequest.RequestJsonSerialize<ResponseDataService>(microServices.SquareHost + microServices.Url, null, ETypeMethod.GET, null,
                    null,null);

            return new CalculateModelView
            {
                Total = Math.Round(valueSquareService.Data.Value * Convert.ToDouble(met), 2).ToString("C"),
                ValueMeter = valueSquareService.Data.Value.ToString("C")
            };
        }
    }
}
