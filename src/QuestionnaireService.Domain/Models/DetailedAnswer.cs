namespace QuestionnaireService.Contracts;

public class DetailedAnswer
{
    public string QuestionWording { get; set; }
    public DetailedOption[] Options { get; set; }
    public int Score { get; set; }
}