using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _2.API.Application;
using _2.API.Models;
using _Support;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace _2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDomainValidation _domainValidation;

        public ImportacaoController(
            IMediator mediator,
            IDomainValidation domainValidation)
        {
            _mediator = mediator;
            _domainValidation = domainValidation;
            _mediator = mediator;
        }

        [HttpPost("{arquivo}")]
        public async Task<ActionResult> PostImportacoes(IFormFile arquivo)
        {
            var mensagensErro = await _mediator.Send(new CreateImportacoesRequest(arquivo));

            if (_domainValidation.IsValid == false)
            {
                return StatusCode(400, mensagensErro);
            }

            return StatusCode(200, "Produtos importados com sucesso!");
        }

        [HttpGet]
        public async Task<IEnumerable<Importacao>> GetImportacoes()
        {
            return await _mediator.Send(new GetImportacoesRequest());
        }

        [HttpGet("{id}")]
        public async Task<Importacao> GetImportacao(Guid id)
        {
            return await _mediator.Send(new GetImportacaoRequest { Id = id });
        }
    }
}
