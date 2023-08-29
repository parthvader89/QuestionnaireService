namespace QuestionnaireService.Contracts;

public class DetailedOption
{
    public string OptionWording { get; set; }
    public bool Selected { get; set; }
    public int Score { get; set; }
}