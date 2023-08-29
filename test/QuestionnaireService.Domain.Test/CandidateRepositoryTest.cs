using FluentAssertions;

namespace QuestionnaireService.Domain.Test;

public class CandidateRepositoryTest
{
    [Fact]
    public void GetCandidateById_WhenCandidateWithIdExists_ReturnsCandidate()
    {
        var sut = new CandidateRepository();

        var result = sut.GetCandidateById(1);

        result.Id.Should().Be(1);
        result.FirstName.Should().Be("Helen");
    }
    
    [Fact]
    public void GetCandidateById_WhenCandidateWithIdDoesntExist_ThrowsException()
    {
        var sut = new CandidateRepository();

        Assert.Throws<Exception>(
            () => sut.GetCandidateById(50)).
            Message.
            Should().
            Be("Unable to find a candidate with the id 50");
    }
}