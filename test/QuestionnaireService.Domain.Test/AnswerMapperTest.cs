using FluentAssertions;
using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain.Test;

public class AnswerMapperTest
{
    [Fact]
    public void MapAnswer_WhenOnlyOneChoiceIsAllowed_ReturnsCorrectScore()
    {
        var testQuestion = new Question()
        {
            Choices = new[]
            {
                new Choice()
                {
                    ChoiceId = 1,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 2,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 3,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 4,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
            },
            Description = "question-wording",
            QuestionId = 1,
            QuestionType = QuestionType.MultipleOptionSingleChoice
        };

        var testAnswer = new Answer()
        {
            QuestionId = 1,
            Selection = new[] { 4 }
        };
        var sut = new AnswerMapper();
        
        var result = sut.MapAnswer(testQuestion, testAnswer);

        result.Score.Should().Be(1);
    }
    
    [Fact]
    public void MapAnswer_WhenMultipleChoicesAreAllowed_ReturnsCorrectScore()
    {
        var testQuestion = new Question()
        {
            Choices = new[]
            {
                new Choice()
                {
                    ChoiceId = 1,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 2,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 3,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 4,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
            },
            Description = "question-wording",
            QuestionId = 1,
            QuestionType = QuestionType.MultipleOptionMultipleChoice
        };

        var testAnswer = new Answer()
        {
            QuestionId = 1,
            Selection = new[] { 1,2,4 }
        };
        var sut = new AnswerMapper();
        
        var result = sut.MapAnswer(testQuestion, testAnswer);

        result.Score.Should().Be(3);
    }
    
    [Fact]
    public void MapAnswer_WhenSingleChoiceIsAllowedButMultipleSupplied_Throws()
    {
        var testQuestion = new Question()
        {
            Choices = new[]
            {
                new Choice()
                {
                    ChoiceId = 1,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 2,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 3,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
                new Choice()
                {
                    ChoiceId = 4,
                    Description = "choice-description-1",
                    IsNoneOfTheAbove = false,
                    Points = 1
                },
            },
            Description = "question-wording",
            QuestionId = 1,
            QuestionType = QuestionType.MultipleOptionSingleChoice
        };

        var testAnswer = new Answer()
        {
            QuestionId = 1,
            Selection = new[] { 1,2,4 }
        };
        var sut = new AnswerMapper();

        Assert.Throws<Exception>(() => sut.MapAnswer(testQuestion, testAnswer)).Message.Should()
            .Be("Only one option allowed for question with id 1");
    }
}