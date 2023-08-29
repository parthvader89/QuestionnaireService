namespace QuestionnaireService.Domain.Models;

public class Choice
{
    public int ChoiceId { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsNoneOfTheAbove { get; set; }
}