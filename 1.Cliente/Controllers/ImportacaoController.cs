using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _1.Cliente.Application;
using MediatR;

namespace _1.Cliente.Controllers
{
    public class ImportacaoController : Controller
    {
        private readonly IMediator _mediator;

        public ImportacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string mensagem)
        {
            ViewBag.Mensagem = mensagem;
            return View(await _mediator.Send(new GetImportacoesRequest()));
        }

        [HttpGet]
        public IActionResult ImportarArquivo()
        {
            return View(new CreateImportacoesRequest());
        } 
        
        [HttpGet]
        public async Task<IActionResult> Visualizar(Guid id)
        {
            return View(await _mediator.Send(new GetImportacaoRequest { Id = id }));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ImportarArquivo(CreateImportacoesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var mensagensErro = await _mediator.Send(request);

            if (mensagensErro.Count == 0)
            {
                return RedirectToAction("Index", new {Mensagem = "Produtos Importados com sucesso!" });
            }

            ViewBag.MensagensErro = mensagensErro;

            return View(request);
        }
    }
}
