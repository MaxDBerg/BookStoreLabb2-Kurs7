using AutoMapper;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.BookDTO;
using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;

namespace MinimalAPI_Books
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookCreateDTO, Book>()
                .ForMember(destination => destination.Id, option => option.Ignore()) // Exclude Id during mapping
                .ForMember(destination => destination.Publication, option => option.Ignore()) // Exclude Publication during mapping
                .ForMember(destination => destination.Genres, option => option.Ignore())
                .ForMember(destination => destination.LanguageId, option => option.MapFrom(source => source.LanguageId))
                .ForMember(destination => destination.AuthorId, option => option.MapFrom(source => source.AuthorId));
            CreateMap<BookUpdateDTO, Book>()
                .ForMember(destination => destination.Publication, option => option.Ignore()) // Exclude Publication during mapping
                .ForMember(destination => destination.Genres, option => option.Ignore())
                .ForMember(destination => destination.LanguageId, option => option.MapFrom(source => source.LanguageId))
                .ForMember(destination => destination.AuthorId, option => option.MapFrom(source => source.AuthorId));
            CreateMap<Book, BookReadDTO>()
                .ForMember(destination => destination.Language, option => option.MapFrom(source => source.Language.Name))
                .ForMember(destination => destination.Author, option => option.MapFrom(source => source.Author.Name));
            CreateMap<Book, BookReadDTOComplete>();
            CreateMap<Book, BookForAuthorReadDTO>();
            CreateMap<Book, BookForGenreReadDTO>();
            CreateMap<Book, BookForLanguageReadDTO>();
            CreateMap<Author, AuthorReadDTO>();
            CreateMap<Author, AuthorWithBooksReadDTO>();
            CreateMap<AuthorCreateDTO, Author>()
                .ForMember(destination => destination.Id, option => option.Ignore()); // Exclude Id during mapping
            CreateMap<AuthorUpdateDTO, Author>();
            CreateMap<Language, LanguageReadDTO>();
            CreateMap<Language, LanguageWithBooksReadDTO>();
            CreateMap<LanguageCreateDTO, Language>()
                .ForMember(destination => destination.Id, option => option.Ignore()); // Exclude Id during mapping
            CreateMap<LanguageUpdateDTO, Language>();
            CreateMap<Genre, GenreReadDTO>();
            CreateMap<Genre, GenreWithBooksReadDTO>();
            CreateMap<GenreCreateDTO, Genre>()
                .ForMember(destination => destination.Id, option => option.Ignore()); // Exclude Id during mapping
            CreateMap<GenreUpdateDTO, Genre>();
        }
    }

}
