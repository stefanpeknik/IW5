using FluentValidation;
using TaHooK.Common.Models.User;

namespace TaHooK.Api.BL.Validators;

public class UserValidator: AbstractValidator<UserDetailModel>
{
    public UserValidator()
    {
    }
}