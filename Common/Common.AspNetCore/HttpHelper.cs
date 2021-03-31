using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.AspNetCore.Model;
using Newtonsoft.Json;

namespace Common.AspNetCore
{
    public static class HttpHelper
    {
        public static async Task<HttpResponseResult<T>> PostAsync<T>(string url, HttpContent content, TimeSpan? timeout = null, AuthenticationHeaderValue authorization = null, Dictionary<string, string> headers = null)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true;

            var httpClient = new HttpClient();

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.PostAsync(url, content))
            {
                return new HttpResponseResult<T>()
                {
                    StatusCode = response.StatusCode,
                    Body = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()),
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }



        public static async Task<HttpResponseResult<T>> PostAsync<T>(HttpClient httpClient, HttpContent content, TimeSpan? timeout = null, AuthenticationHeaderValue authorization = null, Dictionary<string, string> headers = null)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true;

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.PostAsync(httpClient.BaseAddress, content))
            {
                return new HttpResponseResult<T>()
                {
                    StatusCode = response.StatusCode,
                    Body = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()),
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }

        public static async Task<HttpResponseResult<T>> PostAsync<T>(HttpClient httpClient, string body, string contentType, TimeSpan? timeout = null, AuthenticationHeaderValue authorization = null, Dictionary<string, string> headers = null, bool useSsl = false)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true;
            var httpContent = new StringContent(body, Encoding.UTF8, contentType);

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.PostAsync(httpClient.BaseAddress, httpContent))
            {
                var bodyString = await response.Content.ReadAsStringAsync();
                return new HttpResponseResult<T>()
                {
                    StatusCode = response.StatusCode,
                    Body = JsonConvert.DeserializeObject<T>(bodyString),
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }

        public static async Task<HttpResponseResult> PostAsync(
            HttpClient httpClient,
            string body,
            string contentType,
            TimeSpan? timeout = null,
            AuthenticationHeaderValue authorization = null,
            Dictionary<string, string> headers = null,
            bool useSsl = false)
        {

            var httpContent = new StringContent(body, Encoding.UTF8, contentType);

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.PostAsync(httpClient.BaseAddress, httpContent))
            {
                var bodyString = await response.Content.ReadAsStringAsync();

                return new HttpResponseResult()
                {
                    StatusCode = response.StatusCode,
                    Body = bodyString,
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }


        public static async Task<HttpResponseResult> GetAsync(
          HttpClient httpClient,
          TimeSpan? timeout = null,
          AuthenticationHeaderValue authorization = null,
          Dictionary<string, string> headers = null
          )
        {

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.GetAsync(httpClient.BaseAddress))
            {
                var bodyString = await response.Content.ReadAsStringAsync();

                return new HttpResponseResult()
                {
                    StatusCode = response.StatusCode,
                    Body = bodyString,
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }

        public static async Task<HttpResponseResult<T>> GetAsync<T>(
            HttpClient httpClient,
            string route,
            TimeSpan? timeout = null,
            AuthenticationHeaderValue authorization = null,
            Dictionary<string, string> headers = null
        )
        {

            if (authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = authorization;

            if (timeout.HasValue)
                httpClient.Timeout = timeout.Value;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using (var response = await httpClient.GetAsync(route))
            {
                var bodyString = await response.Content.ReadAsStringAsync();

                return new HttpResponseResult<T>()
                {
                    StatusCode = response.StatusCode,
                    Body = string.IsNullOrEmpty(bodyString) ? default(T) : JsonConvert.DeserializeObject<T>(bodyString),
                    ContentType = response.Content.Headers.ContentType?.MediaType
                };
            }
        }
    }
}
