using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace _1.Cliente.Application
{
    internal class CreateImportacoesHandler : IRequestHandler<CreateImportacoesRequest, List<string>>
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateImportacoesHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<string>> Handle(CreateImportacoesRequest request, CancellationToken cancellationToken)
        {
            if (request.Arquivo == null)
            {

                throw new ArgumentException("O arquivo deve ser informado.");
            }

            var formData = new MultipartFormDataContent
            {
                {new StreamContent(request.Arquivo.OpenReadStream()), "arquivo", "arquivo"}
            };

            var requestApi = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44327/api/Importacao/ArquivoExcel")
            {
                Content = formData
            };

            requestApi.Headers.Add("accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(requestApi, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

                return await JsonSerializer.DeserializeAsync
                    <List<string>>(responseStream, cancellationToken: cancellationToken);
            }

            return new List<string> {};

            
        }
    }
}
