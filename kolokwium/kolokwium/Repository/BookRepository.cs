using kolokwium.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace kolokwium.Repository;

public class BookRepository : IBookRepository
{
    private readonly string _connectionString;
 
    public BookRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<Book> AddAsync(Book book)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = "INSERT INTO Books (Title) VALUES (@Title); SELECT SCOPE_IDENTITY();";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", book.Title);
                var newId = await command.ExecuteScalarAsync();
                book.Id = Convert.ToInt32(newId);
                return book;
            }
        }
    }
}