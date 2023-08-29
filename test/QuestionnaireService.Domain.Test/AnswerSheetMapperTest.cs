using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;
using Xunit;

namespace QuestionnaireService.Domain.Test;

public class AnswerSheetMapperTest
{
    [Fact]
    public void Score_WhenPassedWithAnswersAndQuestionnaire_ReturnsCumulativeScoreAndAnswers()
    {
        Questionnaire[] questionnaires;
        using (var file = File.OpenText("data/TestQuestionnaires.json"))
        {
            var serializer = new JsonSerializer();
            questionnaires = serializer.
                                 Deserialize<IEnumerable<Questionnaire>>(new JsonTextReader(file))?.
                                 ToArray() ??
                             throw new InvalidOperationException(
                                 "Unable to load test questionnaire file");
        }

        var testQuestionnaire = questionnaires[0];
        var testRequestAnswerSheet = new Answer[2]
        {
            new Answer() { QuestionId = 1, Selection = new[] { 1 } },
            new Answer() { QuestionId = 2, Selection = new[] { 1, 2 } },
        };
        var mockAnswerMapper = Substitute.For<IAnswerMapper>();
        mockAnswerMapper.MapAnswer(Arg.Any<Question>(), Arg.Any<Answer>()).
            Returns(new DetailedAnswer() 
            {
                Options = new[]
                {
                    new DetailedOption() { OptionWording = "A", Score = 1, Selected = true },
                    new DetailedOption() { OptionWording = "A", Score = 1, Selected = false },
                    new DetailedOption() { OptionWording = "A", Score = 1, Selected = false },
                    new DetailedOption() { OptionWording = "A", Score = 1, Selected = false },
                },
                QuestionWording = "wording-of-question",
                Score = 1
            });
        
        var sut = new AnswerSheetMapper(mockAnswerMapper);

        var result = sut.Map(testRequestAnswerSheet, testQuestionnaire);

        result.Item1.Length.Should().Be(2);
        result.Item2.Equals(2);
    }
    
}