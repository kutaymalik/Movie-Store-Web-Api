using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Store_Web_Api.Entities;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual List<Movie>? PurchasedMovies { get; set; }
    public int FavGenreId { get; set; }
    public virtual Genre? FavGenre { get; set; }
    public virtual List<Order>? Orders { get; set; }
}
