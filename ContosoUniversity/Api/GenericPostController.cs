using ContosoUniversity.Features;
using MediatR.Extensions.Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContosoUniversity.Api
{
    public class GenericPostController<TRequest, TResponse> : MediatrMvcGenericController<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IMediator _mediator;

        public GenericPostController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async override Task<IActionResult> Index(TRequest request)
        {
            await _mediator.Send(request);

            return this.RedirectToActionJson(nameof(Index));
        }
    }

    public class GenericPostController<TRequest> : GenericPostController<TRequest, Unit> where TRequest : IRequest
    {
        private IMediator _mediator;

        public GenericPostController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async override Task<IActionResult> Index(TRequest request)
        {
            await _mediator.Send(request);

            return this.RedirectToActionJson("Index");
        }
    }
}
