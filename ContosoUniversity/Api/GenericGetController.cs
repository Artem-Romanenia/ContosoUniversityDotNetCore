using MediatR.Extensions.Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContosoUniversity.Api
{
    public class GenericGetController<TRequest, TResponse> : MediatrMvcGenericController<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IMediator _mediator;

        public GenericGetController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async override Task<IActionResult> Index(TRequest request)
        {
            return View(await _mediator.Send(request));
        }
    }
}
