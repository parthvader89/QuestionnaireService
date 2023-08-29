using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public interface IAnswerMapper
{
    DetailedAnswer MapAnswer(Question question, Answer answer);
}