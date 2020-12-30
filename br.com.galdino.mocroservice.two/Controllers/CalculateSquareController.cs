using br.com.galdino.mocroservice.two.application.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using br.com.galdino.mocroservice.two.api.Controllers.Base;
using br.com.galdino.mocroservice.two.api.Model.Base;

namespace br.com.galdino.mocroservice.two.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CalculateSquareController : ApiBaseController
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Buscar valor do metro quadrado",
            Description = "Obtém o caulo da metragem enviada.")]
        [SwaggerResponse(200, "Valor do metro quadrado")]
        [SwaggerResponse(400, "Não foi possível buscar o valor do metro")]
        [SwaggerResponse(500, "Erro interno, procure o suporte tecnico.")]
        public async Task<IActionResult> Post(decimal meters, [FromServices] ICalculateSquareValueAppService appService)
        {
            if (meters < 10 ||  meters > 10000)
               return BadRequest(new BaseModelView<object>
                {
                    Success = false,
                    Data = null,
                    Message = "The footage must be between 10 and 10.000"
                });

            return await AutoResult(async () => new BaseModelView<object>
            {
                Data = await appService.Post(meters),
                Message = "Calculation of success."
            });
        }


    }
}
