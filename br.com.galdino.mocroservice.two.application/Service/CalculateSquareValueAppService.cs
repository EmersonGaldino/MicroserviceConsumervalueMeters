using System.Threading.Tasks;
using br.com.galdino.mocroservice.two.application.Interface;
using br.com.galdino.mocroservice.two.domain.core.Interface;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity;

namespace br.com.galdino.mocroservice.two.application.Service
{
    public class CalculateSquareValueAppService : ICalculateSquareValueAppService
    {
        private readonly ICalculateSquareValueService service;
        public CalculateSquareValueAppService(ICalculateSquareValueService service)
        {
            this.service = service;
        }

        public async Task<CalculateModelView> Post(decimal met) => await service.Post(met);

    }
}
