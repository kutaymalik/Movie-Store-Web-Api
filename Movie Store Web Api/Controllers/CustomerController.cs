using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomer;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.DeleteCustomer;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomerDetail;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCustomers()
    {
        GetCustomersQuery query = new(dbContext, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetCustomerDetail(int id)
    {
        GetCustomerDetailQuery query = new(dbContext, mapper);
        query.CustomerId = id;

        GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddCustomer([FromBody] CreateCustomerModel model)
    {
        CreateCustomerCommand command = new(dbContext, mapper);
        command.Model = model;

        CreateCustomerCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerModel model)
    {
        UpdateCustomerCommand command = new(dbContext, mapper);
        command.Model = model;
        command.CustomerId = id;

        UpdateCustomerCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteCustomer(int id)
    {
        DeleteCustomerCommand command = new(dbContext);
        command.CustomerId = id;

        DeleteCustomerCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
