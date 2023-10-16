using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActor;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.DeleteActor;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.UpdateActor;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActors;
using Movie_Store_Web_Api.Application.DirectorOperations.Commands.CreateDirector;
using Movie_Store_Web_Api.Application.DirectorOperations.Commands.DeleteDirector;
using Movie_Store_Web_Api.Application.DirectorOperations.Commands.UpdateDirector;
using Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectorDetail;
using Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectors;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;


    public DirectorController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetDirectors()
    {
        GetDirectorsQuery query = new(dbContext, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetDirectorDetail(int id)
    {
        GetDirectorDetailQuery query = new(dbContext, mapper);
        query.DirectorId = id;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddDirector([FromBody] CreateDirectorModel model)
    {
        CreateDirectorCommand command = new(dbContext);
        command.Model = model;

        CreateDirectorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateDirector(int id, [FromBody] UpdateDirectorModel model)
    {
        UpdateDirectorCommand command = new(dbContext);
        command.Model = model;
        command.DirectorId = id;

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteDirector(int id)
    {
        DeleteDirectorCommand command = new(dbContext);
        command.DirectorId = id;

        DeleteDirectorCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
