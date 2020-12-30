using Newtonsoft.Json;

namespace br.com.galdino.mocroservice.two.domain.crosscouting.Entity.Response
{
    public class ResponseDataService
    {
        [JsonProperty("timerProcessing")]
        public long TimerProcessing { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("value")]
        public long Value { get; set; }
    }
}
