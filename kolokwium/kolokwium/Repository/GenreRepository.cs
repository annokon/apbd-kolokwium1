using Microsoft.Data.SqlClient;

namespace kolokwium.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly string _connectionString;
 
    public GenreRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
 
    public async Task<IEnumerable<string>> GetGenresForBookAsync(int bookId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = "SELECT Name FROM Genres INNER JOIN BookGenres ON Genres.Id = BookGenres.GenreId WHERE BookGenres.BookId = @BookId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                var genres = new List<string>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        genres.Add(reader.GetString(0));
                    }
                }
                return genres;
            }
        }
    }
}