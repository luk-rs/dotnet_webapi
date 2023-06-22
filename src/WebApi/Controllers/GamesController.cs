using GamingApi.Games.CQ;
using GamingApi.Games.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yld.GamingApi.WebApi.Attributes;

namespace Yld.GamingApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public sealed class GamesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [UserAgentHeaderValidation]
    public Task<GameDto[]> Get(int offset = 0, int limit = 2)
    {
        return _mediator.Send(new GetGamesQuery(limit, offset));
    }
}
