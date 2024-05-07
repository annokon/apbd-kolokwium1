namespace kolokwium.Repository;

public interface IGenreRepository
{
    Task<IEnumerable<string>> GetGenresForBookAsync(int bookId);
}