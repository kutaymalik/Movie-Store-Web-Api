using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Store_Web_Api.Entities;

public class Director
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual List<Movie>? DirectedMovies { get; set; }
}
