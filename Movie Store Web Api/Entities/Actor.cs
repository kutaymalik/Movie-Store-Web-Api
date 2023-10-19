using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Store_Web_Api.Entities;

public class Actor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Movie> PlayedMovies { get; set; }
}
