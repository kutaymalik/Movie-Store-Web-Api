using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Store_Web_Api.Entities;

public class Movie
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    public int DirectorId { get; set; }
    public virtual Director Director { get; set; }
    
    public virtual List<Actor>? Actors { get; set; }
}
