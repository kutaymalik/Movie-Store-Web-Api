using AutoMapper;
using Movie_Store_Web_Api.Application.ActorOperations.Commands.CreateActorOperations;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActorDetail;
using Movie_Store_Web_Api.Application.ActorOperations.Queries.GetActors;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.UpdateCustomer;
using Movie_Store_Web_Api.Application.CustomerOperations.Queries.GetCustomers;
using Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectorDetail;
using Movie_Store_Web_Api.Application.DirectorOperations.Queries.GetDirectors;
using Movie_Store_Web_Api.Application.GenreOperations.Commands.CreateGenre;
using Movie_Store_Web_Api.Application.MovieOperations.Queries.GetMovieDetail;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<UpdateCustomerModel, Customer>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<CreateActorModel, Actor>();

            CreateMap<Customer, CustomerDetailViewModel>();
            CreateMap<Customer, CustomerViewModel>();

            CreateMap<Genre, GenreViewModel>();

            CreateMap<Movie, MovieDetailViewModel>();
            CreateMap<Movie, MovieViewModel>();

            CreateMap<Actor, ActorDetailViewModel>();
            CreateMap<Actor, ActorsViewModel>();

            CreateMap<Director, DirectorDetailViewModel>();
            CreateMap<Director, DirectorsViewModel>();
        }
    }
}
