using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public class AnswerSheetMapper: IAnswerSheetMapper
{
    private readonly IAnswerMapper _answerMapper;
        
    public AnswerSheetMapper(IAnswerMapper answerMapper)
    {
        _answerMapper = answerMapper;
    }
    public Tuple<DetailedAnswer[], int> Map(Answer[] requestAnswerSheet, Questionnaire questionnaire)
    {
        int score = 0;
        var result = new DetailedAnswer[requestAnswerSheet.Length];
        var numberOfQuestions = questionnaire.Questions.Length;
        
        for (int i = 0; i < numberOfQuestions; i++)
        {
            result[i] = _answerMapper.MapAnswer(questionnaire.Questions[i], requestAnswerSheet[i]);
            score += result[i].Score;
        }

        return new Tuple<DetailedAnswer[], int>(result, score);
    }
}