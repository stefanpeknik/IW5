﻿using AutoMapper;
using TaHooK.Api.BL.Facades.Interfaces;
using TaHooK.Api.DAL.Entities;
using TaHooK.Api.DAL.UnitOfWork;
using TaHooK.Common.Models.Quiz;

namespace TaHooK.Api.BL.Facades;

public class QuizFacade : CrudFacadeBase<QuizEntity, QuizListModel, QuizDetailModel, QuizCreateUpdateModel>, IQuizFacade
{
    public QuizFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(QuizEntity.Scores)}",
        $"{nameof(QuizEntity.Template)}.{nameof(QuizTemplateEntity.Questions)}"
    };
}