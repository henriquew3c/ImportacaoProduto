using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _1.Cliente.Application;
using _1.Cliente.Support;
using MediatR;

namespace _1.Cliente.Controllers
{
    public class ImportacaoController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidationState _validationState;

        public ImportacaoController(IMediator mediator, IValidationState validationState)
        {
            _mediator = mediator;
            _validationState = validationState;
        }

        public IActionResult Index()
        {
            return View(_mediator.Send(new GetImportacoesRequest()).Result);
        }

        [HttpGet]
        public IActionResult ImportarArquivo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ImportarArquivo(CreateImportacoesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _mediator.Send(request);

            if (_validationState.FillErrors(ModelState))
            {
                return RedirectToAction("Index");
            }

            return View(request);
        }
    }
}
