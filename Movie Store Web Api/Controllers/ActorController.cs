using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActor;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActors;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetActors()
    {
        GetActorsQuery query = new(dbContext, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetActorDetail(int id)
    {
        GetActorDetailQuery query = new(dbContext, mapper);
        query.ActorId = id;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddActor([FromBody] CreateActorModel model)
    {
        CreateActorCommand command = new(dbContext);
        command.Model = model;

        CreateActorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel model)
    {
        UpdateActorCommand command = new(dbContext);
        command.Model = model;
        command.ActorId = id;

        UpdateActorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteActor(int id)
    {
        DeleteActorCommand command = new(dbContext);
        command.ActorId = id;

        DeleteActorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
