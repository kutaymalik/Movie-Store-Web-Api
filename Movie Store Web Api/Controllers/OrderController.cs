using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_Api.Application.MovieOperations.Commands.CreateMovie;
using Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovies;
using Movie_Store_Web_Api.Application.OrderOperations.Commands.PurchaseMovie;
using Movie_Store_Web_Api.Application.OrderOperations.Queries.GetOrders;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;
    public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpPost]
    public IActionResult PurchaseMovie([FromBody] CreatePurchaseModel model)
    {
        PurchaseMovieCommand command = new(dbContext);
        command.Model = model;

        PurchaseMovieCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrdersQuery query = new(dbContext, mapper);

        var result = query.Handle();

        return Ok(result);
    }

}
