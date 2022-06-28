using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.ViewModels.Queries;
using Sozluk.Common.ViewModels.RequestModels;
using Sozluk.Common.ViewModels.RequestModels.Entry;

namespace Sozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>().ReverseMap();
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<CreateEntryCommand, Entry>().ReverseMap();
        CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
        CreateMap<Entry, GetEntriesViewModel>().ForMember(x => x.CommentCount,
            y => y.MapFrom(z => z.EntryComments.Count));
    }
}