﻿using AutoMapper;
using TaHooK.Common.Extensions;
using TaHooK.IdentityProvider.BL.Models;
using TaHooK.IdentityProvider.DAL.Entities;

namespace TaHooK.IdentityProvider.BL.MapperProfiles;

public class AppUserMapperProfile : Profile
{
    public AppUserMapperProfile()
    {
        CreateMap<AppUserCreateModel, AppUserEntity>()
            .Ignore(entity => entity.Active)
            .Ignore(entity => entity.Id)
            .Ignore(entity => entity.NormalizedUserName)
            .Ignore(entity => entity.NormalizedEmail)
            .Ignore(entity => entity.EmailConfirmed)
            .Ignore(entity => entity.PasswordHash)
            .Ignore(entity => entity.SecurityStamp)
            .Ignore(entity => entity.ConcurrencyStamp)
            .Ignore(entity => entity.PhoneNumber)
            .Ignore(entity => entity.PhoneNumberConfirmed)
            .Ignore(entity => entity.TwoFactorEnabled)
            .Ignore(entity => entity.LockoutEnd)
            .Ignore(entity => entity.LockoutEnabled)
            .Ignore(entity => entity.AccessFailedCount);

        CreateMap<AppUserEntity, AppUserDetailModel>();
    }
}
