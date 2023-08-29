namespace QuestionnaireService.Domain.Models;

public class Question
{
    public int QuestionId { get; set; }
    public Choice[] Choices { get; set; }
    public string Description { get; set; }
    public QuestionType QuestionType { get; set; }
}

public enum QuestionType
{
    MultipleOptionMultipleChoice,
    MultipleOptionSingleChoice
}