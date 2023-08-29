namespace QuestionnaireService.Contracts;

public class QuestionnaireScoringRequest
{
    public int CandidateId { get; set; }
    public string QuestionnaireId { get; set; }
    public Answer[] AnswerSheet { get; set; }
}