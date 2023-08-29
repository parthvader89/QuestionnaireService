using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using QuestionnaireService.Contracts;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain.Test;

public class QuestionnaireResponseGeneratorTest
{

    [Fact]
    public void GenerateScoringResult_WhenPassedWithValidAnswers_ReturnsResult()
    {
        var mockQuestionnaireRepository = Setup(out var mockCandidateRepository, out var mockAnswerSheetMapper);
        var testRequest = new QuestionnaireScoringRequest()
        {
            AnswerSheet = new Answer[]
            {
                new Answer() { QuestionId = 1, Selection = new[] { 1 } },
                new Answer() { QuestionId = 2, Selection = new[] { 1, 2 } },
            },
            CandidateId = 1,
            QuestionnaireId = "TEST"
        };
        
        var sut = new QuestionnaireResponseGenerator(
            mockQuestionnaireRepository,
            mockCandidateRepository,
            mockAnswerSheetMapper);

        var result = sut.GenerateResponse(testRequest);

        result.Score.Should().Be(5);
        result.DetailedAnswerSheet.Length.Should().Be(2);
    }

    private static IQuestionnaireRepository Setup(
        out ICandidateRepository mockCandidateRepository,
        out IAnswerSheetMapper mockAnswerSheetMapper)
    {
        Questionnaire[] questionnaires;
        using (var file = File.OpenText("data/TestQuestionnaires.json"))
        {
            var serializer = new JsonSerializer();
            questionnaires = serializer.Deserialize<IEnumerable<Questionnaire>>(new JsonTextReader(file))?.ToArray() ??
                             throw new InvalidOperationException(
                                 "Unable to load test questionnaire file");
        }

        var mockQuestionnaireRepository = Substitute.For<IQuestionnaireRepository>();
        mockCandidateRepository = Substitute.For<ICandidateRepository>();
        mockAnswerSheetMapper = Substitute.For<IAnswerSheetMapper>();
        mockQuestionnaireRepository.GetQuestionnaireById("TEST").Returns(questionnaires[0]);
        mockCandidateRepository.GetCandidateById(1).Returns(new Candidate()
        {
            FirstName = "John",
            LastName = "Smith",
            Id = 1
        });
        mockAnswerSheetMapper.Map(Arg.Any<Answer[]>(), questionnaires[0]).Returns(new Tuple<DetailedAnswer[], int>(
            new DetailedAnswer[]
            {
                new DetailedAnswer()
                {
                    Options = new DetailedOption[]
                    {
                        new DetailedOption() { OptionWording = "A", Score = 1, Selected = true },
                        new DetailedOption() { OptionWording = "B", Score = 1, Selected = false },
                        new DetailedOption() { OptionWording = "C", Score = 1, Selected = false },
                        new DetailedOption() { OptionWording = "D", Score = 1, Selected = false },
                    },
                    Score = 1,
                    QuestionWording = "Q1"
                },
                new DetailedAnswer()
                {
                    Options = new DetailedOption[]
                    {
                        new DetailedOption() { OptionWording = "A", Score = 2, Selected = true },
                        new DetailedOption() { OptionWording = "B", Score = 2, Selected = true },
                        new DetailedOption() { OptionWording = "C", Score = 2, Selected = false },
                        new DetailedOption() { OptionWording = "D", Score = 2, Selected = false },
                    },
                    Score = 4,
                    QuestionWording = "Q2"
                }
            }, 5));
        return mockQuestionnaireRepository;
    }
}