using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public class AnswerMapper : IAnswerMapper
{
    public DetailedAnswer MapAnswer(Question question, Answer answer)
    {
        var detailedAnswers = GetDetailedAnswers(question, answer);

        return new DetailedAnswer()
        {
            QuestionWording = question.Description,
            Options = detailedAnswers.Item1,
            Score = detailedAnswers.Item2
        };
    }

    private Tuple<DetailedOption[], int> GetDetailedAnswers(Question question, Answer answer)
    {
        if (question.QuestionType == QuestionType.MultipleOptionSingleChoice && answer.Selection.Length>1)
        {
            throw new Exception($"Only one option allowed for question with id {question.QuestionId}");
        }

        var score = 0;
        var numberOfOptions = question.Choices.Length;
        var result = new DetailedOption[numberOfOptions];

        for (int i = 0; i < numberOfOptions; i++)
        {
            var questionChoice = question.Choices[i];
            var isChecked = answer.Selection.Contains(questionChoice.ChoiceId);
            if (isChecked) score += questionChoice.Points;
            result[i] = new DetailedOption()
            {
                OptionWording = questionChoice.Description,
                Score = questionChoice.Points,
                Selected = isChecked
            };
        }

        return new Tuple<DetailedOption[], int>(result, score);
    }
}