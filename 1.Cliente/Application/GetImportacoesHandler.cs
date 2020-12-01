using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using _1.Cliente.Models;
using _Support;
using MediatR;

namespace _1.Cliente.Application
{
    internal class GetImportacoesHandler : IRequestHandler<GetImportacoesRequest, IEnumerable<Importacao>>
    {
        private readonly IHttpClientFactory _clientFactory;

        public GetImportacoesHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Importacao>> Handle(GetImportacoesRequest request, CancellationToken cancellationToken)
        {
            var requestApi = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44327/api/Importacao");

            requestApi.Headers.Add("Accept", "application/vnd.github.v3+json");
            requestApi.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(requestApi, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                return await JsonSerializer.DeserializeAsync
                    <IEnumerable<Importacao>>(responseStream, cancellationToken: cancellationToken);
            }
            else
            {
                return Array.Empty<Importacao>();
            }
        }
    }
}
