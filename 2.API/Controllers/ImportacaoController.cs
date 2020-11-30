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
        private readonly IValidationState _validationState;

        public ImportacaoController(
            IMediator mediator,
            IValidationState validationState)
        {
            _mediator = mediator;
            _validationState = validationState;
            _mediator = mediator;
        }

        [HttpPost("{arquivo}")]
        public async Task<ActionResult> PostImportacoes(IFormFile arquivo)
        {
            await _mediator.Send(new CreateImportacoesRequest(arquivo));

            if (_validationState.FillErrors(ModelState))
            {
                return StatusCode(400, "Problemas de Validação");
            }

            return StatusCode(200, "Produtos importados com sucesso!");
        }

        [HttpGet]
        public async Task<PaginationResult<Importacao>> GetImportacoes()
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
