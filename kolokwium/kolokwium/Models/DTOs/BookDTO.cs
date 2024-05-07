namespace kolokwium.Models.DTOs;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; }
}

public class Genre
{
    public int Id { get; set; }
}