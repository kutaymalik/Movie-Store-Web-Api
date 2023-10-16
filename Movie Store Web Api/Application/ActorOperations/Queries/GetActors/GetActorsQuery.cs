﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;

namespace Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActors;

public class GetActorsQuery
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly IMapper mapper;

    public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<ActorsViewModel> Handle()
    {
        var actorsList = dbContext.Actors
            .OrderBy(x => x.Id).ToList();

        List<ActorsViewModel> vm = mapper.Map<List<ActorsViewModel>>(actorsList);

        return vm;
    }

}

public class ActorsViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
