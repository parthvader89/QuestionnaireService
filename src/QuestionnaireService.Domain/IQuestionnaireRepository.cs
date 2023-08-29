using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public interface IQuestionnaireRepository
{
    Questionnaire GetQuestionnaireById(string id);
}