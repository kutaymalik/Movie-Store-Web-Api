using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_Api.Application.MovieOperations.Commands.CreateMovie;
using Movie_Store_Web_Api.Application.MovieOperations.Commands.DeleteMovie;
using Movie_Store_Web_Api.Application.MovieOperations.Commands.UpdateMovie;
using Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovieDetail;
using Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovies;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public MovieController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        GetMoviesQuery query = new(dbContext, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetMovieDetail(int id)
    {
        GetMovieDetailQuery query = new(dbContext, mapper);
        query.MovieId = id;

        GetMovieDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] CreateMovieModel model)
    {
        CreateMovieCommand command = new(dbContext, mapper);
        command.Model = model;

        CreateMovieCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel model)
    {
        UpdateMovieCommand command = new(dbContext, mapper);
        command.Model = model;
        command.MovieId = id;

        UpdateMovieCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteMovie(int id)
    {
        DeleteMovieCommand command = new(dbContext);
        command.MovieId = id;

        DeleteMovieCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
