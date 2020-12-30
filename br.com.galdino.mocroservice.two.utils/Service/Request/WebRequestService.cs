using br.com.galdino.mocroservice.two.domain.crosscouting.Enum;
using br.com.galdino.mocroservice.two.domain.crosscouting.Exceptions;
using br.com.galdino.mocroservice.two.utils.Interface.Request;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace br.com.galdino.mocroservice.two.utils.Service.Request
{
    public class WebRequestService : IWebRequestService
    {
        private readonly HttpClient api;
        public WebRequestService(HttpClient httpClient)
        {
            this.api = httpClient;
        }

        #region .::Mehtods
        public async Task<T> RequestJsonSerialize<T>(
            string url,
            object jsonData,
            ETypeMethod metodo,
            string token = null,
            List<KeyValuePair<string, string>> parametters = null,
            IEnumerable<KeyValuePair<string, string>> valuePairs = null) where T : class
        {

            HttpResponseMessage ret = null;
            Dispose();

            if (!string.IsNullOrEmpty(token)) api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (parametters != null && parametters.Count > 0)
            {
                foreach (var item in parametters)
                    api.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            ret = metodo switch
            {
                ETypeMethod.GET => await api.GetAsync(url),
                ETypeMethod.POST => await api.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json")),
                ETypeMethod.PUT => await api.PutAsync(url,
                    new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json")),
                ETypeMethod.DELETE => await api.DeleteAsync(url),
                ETypeMethod.POSTFORM => await api.PostAsync(url, new FormUrlEncodedContent(valuePairs)),
                _ => throw new ArgumentOutOfRangeException(nameof(metodo), metodo, null)
            };

            var returnStr = await ret.Content.ReadAsStringAsync();

            if (ret.StatusCode != HttpStatusCode.OK && !ret.IsSuccessStatusCode)
            {
                throw new RequestException((int)ret.StatusCode, returnStr);
            }

            if (!ret.IsSuccessStatusCode || ret.StatusCode == HttpStatusCode.NoContent)
                return null;

            if (string.IsNullOrEmpty(returnStr) && !ret.IsSuccessStatusCode) throw new RequestException((int)ret.StatusCode, ret.ReasonPhrase);

            return JsonConvert.DeserializeObject<T>(returnStr);
        }

        public void Dispose()
        {
            api.DefaultRequestHeaders.Clear();
        }

        #endregion
    }
}
