using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>().ReverseMap();
    }
}