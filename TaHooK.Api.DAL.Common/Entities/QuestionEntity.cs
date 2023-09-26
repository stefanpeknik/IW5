﻿namespace TaHooK.Api.DAL.Common.Entities;

public record QuestionEntity : EntityBase
{
    public required string Text { get; set; }

    public ICollection<AnswerEntity> Answers { get; set; } = new List<AnswerEntity>();

    public required Guid QuizId { get; set; }
    public required QuizEntity Quiz { get; set; }
}
