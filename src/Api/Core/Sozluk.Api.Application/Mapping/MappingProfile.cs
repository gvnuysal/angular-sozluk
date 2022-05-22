﻿using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.ViewModels.Queries;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>().ReverseMap();
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
    }
}