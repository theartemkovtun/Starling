using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Starling.WebApi.Controllers
{
    public abstract class MediatorController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;

        protected string Username => HttpContext.User.Identity.Name;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}