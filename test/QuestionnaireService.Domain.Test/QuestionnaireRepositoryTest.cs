using FluentAssertions;

namespace QuestionnaireService.Domain.Test;

public class QuestionnaireRepositoryTest
{
    [Fact]
    public void GetQuestionnaireById_WhenQuestionnaireExists_ReturnsQuestionnaire()
    {
        var sut = new QuestionnaireRepository();

        var result = sut.GetQuestionnaireById("FIRSTQUESTIONNAIRE");

        result.Should().NotBeNull();
        result.Questions.Length.Should().Be(2);
    }
    
    [Fact]
    public void GetQuestionnaireById_WhenQuestionnaireDoesntExist_ThrowsException()
    {
        var sut = new QuestionnaireRepository();

        Assert.Throws<Exception>(() => sut.GetQuestionnaireById("NONEXISTENT")).
            Message.
            Should().
            Be("Unable to find questionnaire with the id NONEXISTENT");
    }
}