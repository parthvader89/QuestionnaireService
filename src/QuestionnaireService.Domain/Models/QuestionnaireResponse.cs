namespace QuestionnaireService.Contracts;

public class QuestionnaireResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CandidateId { get; set; }
    public DateTime SubmissionTime { get; set; }
    public string QuestionnaireDescription { get; set; }
    public DetailedAnswer[] DetailedAnswerSheet { get; set; }
    public int Score { get; set; }
}