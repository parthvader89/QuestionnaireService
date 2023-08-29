namespace QuestionnaireService.Domain.Models;

public class Questionnaire
{
    public string QuestionnaireId { get; set; }
    public Question[] Questions { get; set; }
    public string Description { get; set; }
}