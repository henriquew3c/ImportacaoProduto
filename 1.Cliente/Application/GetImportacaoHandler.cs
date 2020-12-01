using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using _1.Cliente.Models;
using MediatR;

namespace _1.Cliente.Application
{
    internal class GetImportacaoHandler : IRequestHandler<GetImportacaoRequest, Importacao>
    {
        private readonly IHttpClientFactory _clientFactory;

        public GetImportacaoHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Importacao> Handle(GetImportacaoRequest request, CancellationToken cancellationToken)
        {
            var requestApi = new HttpRequestMessage(HttpMethod.Get,
                $"https://localhost:44327/api/Importacao/{request.Id}");

            requestApi.Headers.Add("Accept", "application/vnd.github.v3+json");
            requestApi.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(requestApi, cancellationToken);

            if (!response.IsSuccessStatusCode) return null;

            await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync
                <Importacao>(responseStream, cancellationToken: cancellationToken);

        }
    }
}
