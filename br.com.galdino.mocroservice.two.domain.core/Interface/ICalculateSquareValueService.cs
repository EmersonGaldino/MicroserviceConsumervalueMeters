using System.Threading.Tasks;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity;

namespace br.com.galdino.mocroservice.two.domain.core.Interface
{
    public interface ICalculateSquareValueService
    {
        Task<CalculateModelView> Post(decimal met);
    }
}
