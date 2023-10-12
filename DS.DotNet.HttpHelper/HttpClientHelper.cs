using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DS.DotNet.HttpHelper
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientHelper> _logger;

        public HttpClientHelper(HttpClient httpClient, ILogger<HttpClientHelper> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }

        #region Get

        public async Task<T> GetAsync<T>(
            string url,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            CancellationToken cancellationToken = default)
        {
            try
            {
                AddAuthorization(authorizationKeyName, authorizationKeyValue, authorizationKeyValuePrefix);

                var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    return Deserialize<T>(content);
                }
                else
                {
                    throw await CreateError("GET", url, response);
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred when sending GET request to {url}";
                _logger.LogError(ex, errorMessage);
                throw new HttpRequestException(errorMessage, ex);
            }
        }

        #endregion

        #region Post

        public async Task<T> PostAsync<T>(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<T>(
                url, 
                Serialize(data), 
                authorizationKeyValue, 
                authorizationKeyName, 
                authorizationKeyValuePrefix, 
                contentType, 
                cancellationToken);
        }

        public async Task<T> PostAsync<T>(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            var response = await PostAsync(
                url, 
                dataJson, 
                authorizationKeyValue, 
                authorizationKeyName, 
                authorizationKeyValuePrefix, 
                contentType, 
                cancellationToken).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return Deserialize<T>(result);
        }

        public async Task<HttpResponseMessage> PostAsync(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            try
            {
                AddAuthorization(authorizationKeyName, authorizationKeyValue, authorizationKeyValuePrefix);
                EnsureContentType(contentType);

                var response = await _httpClient.PostAsync(url, CreateContent(contentType, dataJson), cancellationToken).ConfigureAwait(false);

                if(response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw await CreateError("POST", url, response);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred when sending PUT request to {url}";
                _logger.LogError(ex, errorMessage);
                throw new HttpRequestException(errorMessage, ex);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            return await PostAsync(url, Serialize(data), authorizationKeyValue, authorizationKeyName, authorizationKeyValuePrefix, contentType, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Put

        public async Task<T> PutAsync<T>(
            string url,
            object content,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            return await PutAsync<T>(url, Serialize(content), authorizationKeyValue, authorizationKeyName, authorizationKeyValuePrefix, contentType, cancellationToken);
        }

        public async Task<T> PutAsync<T>(
            string url,
            string contentJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await PutAsync(url, contentJson, authorizationKeyValue, authorizationKeyName, authorizationKeyValuePrefix, contentType, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    return Deserialize<T>(content);
                }
                else
                {
                    throw await CreateError("PUT", url, response);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred when sending PUT request to {url}";
                _logger.LogError(ex, errorMessage);
                throw new HttpRequestException(errorMessage, ex);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(
            string url,
            string contentJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            try
            {
                AddAuthorization(authorizationKeyName, authorizationKeyValue, authorizationKeyValuePrefix);
                EnsureContentType(contentType);

                var response = await _httpClient.PutAsync(url, CreateContent(contentType, contentJson), cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw await CreateError("PUT", url, response);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred when sending PUT request to {url}";
                _logger.LogError(ex, errorMessage);
                throw new HttpRequestException(errorMessage, ex);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(
            string url,
            object content,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default)
        {
            return await PutAsync(url, Serialize(content), authorizationKeyValue, authorizationKeyName, authorizationKeyValuePrefix, contentType, cancellationToken);
        }

        #endregion

        #region Delete

        public async Task<HttpStatusCode> DeleteAsync(
            string url,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            CancellationToken cancellationToken = default)
        {
            try
            {
                AddAuthorization(authorizationKeyName, authorizationKeyValue, authorizationKeyValuePrefix);

                var response = await _httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
                else
                {
                    throw await CreateError("Delete", url, response);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred when sending DELETE request to {url}";
                _logger.LogError(ex, errorMessage);
                throw new HttpRequestException(errorMessage, ex);
            }
        }

        #endregion

        private void AddAuthorization(string key, string value, string authorizationKeyValuePrefix)
        {
            if (!string.IsNullOrWhiteSpace(value) && !_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add(key, $"{authorizationKeyValuePrefix}{value}");
            }
        }

        private void EnsureContentType(string contentType)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Accept"))
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", contentType);
            }
        }

        private static StringContent CreateContent(string contentType, string jsonContent)
        {
            return new StringContent(jsonContent, Encoding.UTF8, contentType);
        }

        private static T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return default;

            return JsonSerializer.Deserialize<T>(content);
        }

        private static string Serialize(object obj) => JsonSerializer.Serialize(obj);

        private async Task<Exception> CreateError(string method, string route, HttpResponseMessage response)
        {
            var errorContent = response.Content != null ? await response.Content.ReadAsStringAsync() : null;
            string exceptionMessage = @$"An Exception occured at {method} Url: {route} and returned Status Code: {response.StatusCode} with Message: {errorContent ?? "Empty message returned"}.";
            return new Exception(exceptionMessage);
        }
    }
}
