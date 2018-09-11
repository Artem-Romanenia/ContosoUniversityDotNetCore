using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Features.Instructors
{
    public class InstructorsController : Controller
    {
        private readonly IMediator _mediator;

        public InstructorsController(IMediator mediator) => _mediator = mediator;

        //Part of the Instructors Controller is kept here intentionally to demonstrate another aspect of MediatR Mvc behavior.
        //As CreateEdit.Query and CreateEdit.Command requests are handled here explicitly, they will be ignored by MediatR Mvc.
        //Urls GET: /Instructors/CreateEdit/{id} and POST: /Instructors/CreateEdit will not be available.
        //This behavior can be disabled by using DisableHandledRequestDiscovery* methods in Mediatr Mvc feature provider settings.
        //See github Wiki for more info.

        public async Task<IActionResult> Create()
            => View(nameof(CreateEdit), await _mediator.Send(new CreateEdit.Query()));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEdit.Command command)
        {
            await _mediator.Send(command);

            return this.RedirectToActionJson(nameof(Index));
        }

        public async Task<IActionResult> Edit(CreateEdit.Query query)
            => View(nameof(CreateEdit), await _mediator.Send(query));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEdit.Command command)
        {
            await _mediator.Send(command);

            return this.RedirectToActionJson(nameof(Index));
        }
    }
}
