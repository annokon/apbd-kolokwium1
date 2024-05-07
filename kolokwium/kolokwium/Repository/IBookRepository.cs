using kolokwium.Models.DTOs;

namespace kolokwium.Repository;

public interface IBookRepository
{
    Task<Book> AddAsync(Book book);
}