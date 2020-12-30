using br.com.galdino.mocroservice.two.domain.crosscouting.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace br.com.galdino.mocroservice.two.utils.Interface.Request
{
    public interface IWebRequestService
    {
        Task<T> RequestJsonSerialize<T>(
            string url,
            object jsonData,
            ETypeMethod method,
            string token = null,
            List<KeyValuePair<string, string>> parameters = null,
            IEnumerable<KeyValuePair<string, string>> valuePairs = null) where T : class;
    }
}
