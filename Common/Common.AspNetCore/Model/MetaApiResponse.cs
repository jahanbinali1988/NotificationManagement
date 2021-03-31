using System;
using Newtonsoft.Json;

namespace Common.AspNetCore.Model
{
    public class MetaApiResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalItemsCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? RemainingItemsCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NextUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VideosNextUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OperationResult { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonIgnore]
        public string CustomDisplayMessage { get; set; }

        public string DisplayMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Fields { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception Exception { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessCode { get; set; }

    }
}