using QuestionnaireService.Contracts;

namespace QuestionnaireService.Domain;

public class QuestionnaireResponseGenerator : IQuestionnaireResponseGenerator
{
    private readonly IQuestionnaireRepository _questionnaireQuestionnaireRepository;
    private readonly  ICandidateRepository _candidateRepository;
    private IAnswerSheetMapper _answerSheetMapper;

    public QuestionnaireResponseGenerator(
        IQuestionnaireRepository questionnaireQuestionnaireRepository, 
        ICandidateRepository candidateRepository, 
        IAnswerSheetMapper answerSheetMapper)
    {
        _questionnaireQuestionnaireRepository = questionnaireQuestionnaireRepository;
        _candidateRepository = candidateRepository;
        _answerSheetMapper = answerSheetMapper;
    }

    public QuestionnaireResponse GenerateResponse(QuestionnaireScoringRequest request)
    {
        var candidate = _candidateRepository.GetCandidateById(request.CandidateId);
        var questionnaire = _questionnaireQuestionnaireRepository.GetQuestionnaireById(request.QuestionnaireId);
        var answerSheetResult = _answerSheetMapper.Map(request.AnswerSheet, questionnaire);

        return new QuestionnaireResponse()
        {
            CandidateId = candidate.Id,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            QuestionnaireDescription = questionnaire.Description,
            DetailedAnswerSheet = answerSheetResult.Item1,
            Score = answerSheetResult.Item2
        };
    }
}