using System.Net;

namespace DS.DotNet.HttpHelper
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(
            string url,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            CancellationToken cancellationToken = default);

        Task<T> PostAsync<T>(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PostAsync<T>(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PostAsync(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PostAsync(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PutAsync<T>(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PutAsync<T>(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PutAsync(
            string url,
            string dataJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PutAsync(
            string url,
            object data,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<HttpStatusCode> DeleteAsync(
            string url,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            CancellationToken cancellationToken = default);
    }
}
