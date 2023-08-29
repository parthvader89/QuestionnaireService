using QuestionnaireService.Contracts;

namespace QuestionnaireService.Domain;

public interface IQuestionnaireResponseGenerator
{
    QuestionnaireResponse GenerateResponse(QuestionnaireScoringRequest request);
}