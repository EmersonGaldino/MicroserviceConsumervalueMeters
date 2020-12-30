using System.Threading.Tasks;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity;

namespace br.com.galdino.mocroservice.two.application.Interface
{
    public interface ICalculateSquareValueAppService
    {
        Task<CalculateModelView> Post(decimal met);
    }
}
