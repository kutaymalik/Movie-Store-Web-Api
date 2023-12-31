﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQuery
{
    private readonly IMovieStoreDbContext dbContext;
    public int ActorId { get; set; }
    private readonly IMapper mapper;

    public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public Actor Handle()
    {
        var actor = dbContext.Actors
            .Where(x => x.Id == ActorId).Include(x => x.PlayedMovies).SingleOrDefault();

        if (actor == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        ActorDetailViewModel vm  = mapper.Map<ActorDetailViewModel>(actor);

        return actor;
    }
}

public class ActorDetailViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Movie> PlayedMovies { get; set; }
}
