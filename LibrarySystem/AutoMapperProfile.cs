using AutoMapper;
using LibrarySystem.DTOs;
using LibrarySystem.Model;

namespace LibrarySystem;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Author, AuthorDTO>();
        CreateMap<AuthorDTO, Author>();
        CreateMap<Author, SimpleAuthorDTO>();
        CreateMap<SimpleAuthorDTO, Author>();
        CreateMap<Book, BookDTO>();
        CreateMap<BookDTO, Book>();
        CreateMap<Book, SimpleBookDTO>();
        CreateMap<SimpleBookDTO, Book>();
    }
}