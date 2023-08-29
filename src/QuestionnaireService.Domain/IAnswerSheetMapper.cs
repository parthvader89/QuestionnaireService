using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public interface IAnswerSheetMapper
{
    Tuple<DetailedAnswer[], int> Map(Answer[] requestAnswerSheet, Questionnaire questionnaire);
}