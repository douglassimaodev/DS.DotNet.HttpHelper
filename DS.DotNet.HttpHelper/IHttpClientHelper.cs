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
            object content,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PostAsync<T>(
            string url,
            string contentJson,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PutAsync<T>(
            string url,
            object content,
            string authorizationKeyValue = "",
            string authorizationKeyName = "Authorization",
            string authorizationKeyValuePrefix = "Bearer ",
            string contentType = "application/json",
            CancellationToken cancellationToken = default);

        Task<T> PutAsync<T>(
            string url,
            string contentJson,
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
